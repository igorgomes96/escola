using ServidorWS.Exceptions;
using ServidorWS.Models;
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
                    // Lê o arquivo xml, fazendo o parse e adicionando cada um a alunosList
                    AlunoListXML alunosList = ReadFile(file.LocalFileName);
                    // Verifica a integridade das informações e salva os dados no banco
                    SaveAlunos(alunosList);

                    // Adiciona um registro de log na tabela de Importacao
                    db.Importacao.Add(new Importacao
                    {
                        DataHora = DateTime.Now,
                        NomeArquivo = file.Headers.ContentDisposition.FileName.Replace("\"", ""),
                    });

                    // Salva as alterações no banco
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                finally
                {
                    File.Delete(file.LocalFileName);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Dados importados com sucesso!");
        }

        private AlunoListXML ReadFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AlunoListXML));
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            AlunoListXML alunosList = (AlunoListXML)serializer.Deserialize(fileStream);
            fileStream.Close();
            return alunosList;
        }

        private void SaveAlunos(AlunoListXML alunosList)
        {
            int index = 1;
            alunosList.Alunos.ForEach(aluno => {

                if (aluno.CPFAluno == "") throw new CPFNaoInformadoException(index);
                if (aluno.NomeAluno == "") throw new NomeNaoInformadoException(index);
                if (aluno.Endereco == "") throw new EnderecoNaoInformadoException(index);

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
