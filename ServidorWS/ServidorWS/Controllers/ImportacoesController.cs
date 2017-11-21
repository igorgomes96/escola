using ServidorWS.Dto;
using ServidorWS.Exceptions;
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
using System.Web.Http.Description;
using System.Xml.Serialization;

namespace ServidorWS.Controllers
{
    public class ImportacoesController : ApiController
    {
        private readonly IImportacaoService _importacaoService;

        public ImportacoesController(IImportacaoService importacaoService)
        {
            _importacaoService = importacaoService; //?? throw new ArgumentNullException(nameof(importacaoService));
        }

        [ResponseType(typeof(IEnumerable<ImportacaoDto>))]
        public IHttpActionResult GetAll()
        {
            try { 
                return Ok(_importacaoService.ListAll());
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [ResponseType(typeof(ImportacaoDto))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(_importacaoService.Delete(id));
            } catch (EntidadeNaoEncontradaException<int, Importacao> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
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

            ICollection<Aluno> alunos = null;

            // Para cada arquivo enviado
            foreach (MultipartFileData file in provider.FileData)
            {
                try
                {
                    //Importa o arquivo
                    alunos = _importacaoService.Importar(file.LocalFileName, provider.FormData["nomeArquivo"]);
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
            }
            return Request.CreateResponse(HttpStatusCode.OK, alunos);
        }

        [HttpGet]
        [Route("api/Importacoes/{codigo}/Download")]
        public HttpResponseMessage DonwloadImportacao(int codigo)
        {
            HttpResponseMessage result = null;

            try { 
                Importacao imp = _importacaoService.Find(codigo);
                if (imp == null) return new HttpResponseMessage(HttpStatusCode.NotFound);

                var localFilePath = HttpContext.Current.Server.MapPath("~/App_Data") + imp.NomeArquivo;
                byte[] arq = imp.Arquivo.ToArray();

                File.WriteAllBytes(localFilePath, arq);

                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = imp.NomeArquivo
                };
                result.Content.Headers.Add("Access-Control-Expose-Headers", "x-filename"); //Sem ele, o header 'x-filename' não aparece
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");
                result.Content.Headers.Add("x-filename", imp.NomeArquivo);

                return result;
            } catch (EntidadeNaoEncontradaException<int, Importacao> e)
            {
                result = Request.CreateErrorResponse(HttpStatusCode.NotFound, e);
                return result;
            } catch (Exception e)
            {
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                return result;
            }

        }


    }
}
