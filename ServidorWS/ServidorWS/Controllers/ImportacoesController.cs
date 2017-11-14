﻿using ServidorWS.Exceptions;
using ServidorWS.Models;
using ServidorWS.Services;
using ServidorWS.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Serialization;

namespace ServidorWS.Controllers
{
    public class ImportacoesController : ApiController
    {
        private Contexto db = new Contexto();
        private readonly IImportacaoService _importacaoService;

        public ImportacoesController(IImportacaoService importacaoService)
        {
            _importacaoService = importacaoService ?? throw new ArgumentNullException(nameof(importacaoService));
        }

        [HttpPost]
        [Route("api/Importacoes/Upload")]
        public async Task<HttpResponseMessage> Upload()
        {
            // Verifica se request contém multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            //Diretório App_Data, para salvar o arquivo temporariamente
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            // Lê o arquivo da requisição assincronamente
            await Request.Content.ReadAsMultipartAsync(provider);

            // Para cada arquivo enviado
            foreach (MultipartFileData file in provider.FileData)
            {
                try
                {
                    /*
                    // Lê o arquivo xml, fazendo o parse e adicionando cada um a alunosList
                    AlunoListXML alunosList = ReadFile(file.LocalFileName);

                    // Verifica a integridade das informações e salva os dados no banco
                    SaveAlunos(alunosList);

                    // Adiciona um registro na tabela de Importacao, para histórico de importações
                    db.Importacao.Add(new Importacao
                    {
                        DataHora = DateTime.Now,
                        NomeArquivo = file.Headers.ContentDisposition.FileName.Replace("\"", ""),
                    });

                    // Salva as alterações no banco
                    db.SaveChanges();
                    */

                    _importacaoService.Importar(file.LocalFileName);

                }
                catch (CPFNaoInformadoException e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CPF não informado para o " + e.IndexAluno + "º aluno da base!");
                }
                catch (CPFFormatoIncorretoException e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "O CPF deve ter 11 caracteres! (" + e.IndexAluno + "º aluno da base, CPF informado: " + e.CPF + ")");
                }
                catch (NomeNaoInformadoException e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome não informado! (" + e.IndexAluno + "º aluno da base, CPF informado: " + e.CPF + ")");
                }
                catch (EnderecoNaoInformadoException e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Endereço não informado! (" + e.IndexAluno + "º aluno da base, CPF informado: " + e.CPF + ")");
                }
                catch (InvalidOperationException e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message + ' ' + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                finally
                {
                    try
                    {
                        File.Delete(file.LocalFileName);
                    } catch (Exception) { }
                    
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Dados importados com sucesso!");
        }

        private AlunoListXML ReadFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AlunoListXML));
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            AlunoListXML alunosList;
            try
            {
                alunosList = (AlunoListXML)serializer.Deserialize(fileStream);
            } catch (Exception e) {
                throw e;
            } finally
            {
                fileStream.Close();
            }
            
            return alunosList;
        }

        private void SaveAlunos(AlunoListXML alunosList)
        {
            int index = 1;
            alunosList.Alunos.ForEach(aluno => {

                aluno.CPFAluno = aluno.CPFAluno.Trim();
                if (aluno.CPFAluno == "") throw new CPFNaoInformadoException(index);
                if (aluno.CPFAluno.Length != 11) throw new CPFFormatoIncorretoException(index, aluno.CPFAluno);
                if (aluno.NomeAluno == "") throw new NomeNaoInformadoException(index, aluno.CPFAluno);
                if (aluno.Endereco == "") throw new EnderecoNaoInformadoException(index, aluno.CPFAluno);

                Aluno alunoModel = aluno.GetAlunoModel();

                if (AlunoExists(alunoModel.CPFAluno))
                    db.Entry(alunoModel).State = System.Data.Entity.EntityState.Modified;
                else
                    db.Entry(alunoModel).State = System.Data.Entity.EntityState.Added;

                index++;
            });
        }

        private bool AlunoExists(string cpf)
        {
            return db.Aluno.Where(x => x.CPFAluno == cpf).Count() > 0;
        }

    }
}
