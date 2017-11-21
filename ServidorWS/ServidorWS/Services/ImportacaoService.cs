using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWS.Models;
using System.Xml.Serialization;
using System.IO;
using ServidorWS.XML;
using ServidorWS.Exceptions;
using ServidorWS.Repository;
using ServidorWS.Dto;
using ServidorWS.Mapping;

namespace ServidorWS.Services
{
    public class ImportacaoService : IImportacaoService
    {
        private readonly IAlunosService _alunosService;
        private readonly IGenericRepository<int, Importacao> _importacaoRepositoy;
        private readonly IMapper<ImportacaoDto, Importacao> _importacaoMapper;
        private readonly IMapper<AlunoXML, Aluno> _alunoMapper;

        /// <summary>
        /// Injeção das Dependências
        /// </summary>
        /// <param name="alunosService"></param>
        /// <param name="importacaoRepository"></param>
        public ImportacaoService(IAlunosService alunosService, 
            IGenericRepository<int, Importacao> importacaoRepository, 
            IMapper<ImportacaoDto, Importacao> importacaoMapper, 
            IMapper<AlunoXML, Aluno> alunoMapper)
        {
            _alunosService = alunosService; //?? throw new ArgumentNullException(nameof(alunosService));
            _importacaoRepositoy = importacaoRepository; //?? throw new ArgumentNullException(nameof(importacaoRepository));
            _importacaoMapper = importacaoMapper;
            _alunoMapper = alunoMapper;
        }

        public ImportacaoDto Delete(int codigo)
        {
            return _importacaoMapper.Map(_importacaoRepositoy.Delete(codigo));
        }

        public Importacao Find(int codigo)
        {
            return _importacaoRepositoy.Find(codigo);
        }

        /// <summary>
        /// Lê o arquivo XML, faz o parse, salva os dados lidos, e retorna a lista de alunos salvos.
        /// </summary>
        /// <param name="path">Path do arquivo XML</param>
        /// <returns>Lista de alunos importados</returns>
        /// <exception cref="CPFNaoInformadoException">CPF do aluno não informado.</exception>
        /// <exception cref="CPFFormatoIncorretoException">CPF fora do padrão (Formato Inválido)</exception>
        /// <exception cref="NomeNaoInformadoException">Nome do aluno não informado.</exception>
        /// <exception cref="EnderecoNaoInformadoException">Endereço do aluno não informado.</exception>
        /// <exception cref="InvalidOperationException">Erro ao ler o arquivo.</exception>
        public ICollection<Aluno> Importar(string path, string nomeArquivo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AlunoListXML));
            FileStream fileStream = new FileStream(path, FileMode.Open);
            AlunoListXML alunosList;

            //Tenta ler o arquivo e salvar. Qualquer exceção, fecha o arquivo e lança a exceção.
            try
            { 
                alunosList = (AlunoListXML)serializer.Deserialize(fileStream);
                return SaveAlunos(alunosList);
            }
            finally
            {
                //Fecha e exclui o arquivo
                fileStream.Close();

                //Insere um registro na tabela Importacoes para manter o histórico de Importações.
                Importacao imp = new Importacao
                {
                    DataHora = DateTime.Now,
                    NomeArquivo = nomeArquivo,
                    Arquivo = File.ReadAllBytes(path) //Lê os bytes do arquivo e salva, para possibilitar posterior download
                };
                _importacaoRepositoy.Save(imp);

                File.Delete(path);
            }

        }

        ICollection<ImportacaoDto> IImportacaoService.ListAll()
        {
            return _importacaoMapper.Map(_importacaoRepositoy.ListAll());
        }

        /// <summary>
        /// Recebe a lista de Alunos XML, da forma como foram lidos, e mapeia para objetos Entitdade, para então salvar.
        /// </summary>
        /// <param name="alunoListXML">Informações lidas do arquivo</param>
        /// <returns>Lista de Objetos Entitdade (ICollection<Aluno>) salvos</returns>
        private ICollection<Aluno> SaveAlunos(AlunoListXML alunoListXML)
        {
            int index = 1;

            ICollection<Aluno> alunosEntity = alunoListXML.Alunos.Select(aluno =>
                {
                    aluno.CPFAluno = aluno.CPFAluno.Trim();
                    if (aluno.CPFAluno == "") throw new CPFNaoInformadoException(index);
                    if (aluno.CPFAluno.Length != 11) throw new CPFFormatoIncorretoException(index, aluno.CPFAluno);
                    if (aluno.NomeAluno == "") throw new NomeNaoInformadoException(index, aluno.CPFAluno);
                    if (aluno.Endereco == "") throw new EnderecoNaoInformadoException(index, aluno.CPFAluno);

                    index++;

                    return _alunoMapper.Map(aluno); //Mapeia para um objeto entidade
                }).ToList();

            _alunosService.SaveAll(alunosEntity);

            return alunosEntity;
        }
    }
}