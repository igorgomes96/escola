using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServidorWS.Models;

namespace ServidorWS.Controllers
{
    public class AlunosController : ApiController
    {
        private Contexto db = new Contexto();

        public IEnumerable<Aluno> GetAluno()
        {
            return db.Aluno.ToList();
        }

        
        [ResponseType(typeof(Aluno))]
        public IHttpActionResult DeleteAluno(string id)
        {
            Aluno aluno = db.Aluno.Find(id);
            if (aluno == null)
            {
                return Content(HttpStatusCode.NotFound, "O CPF do aluno (" + id + ") não foi encontrado na nossa base de dados!");
            }

            db.Aluno.Remove(aluno);
            db.SaveChanges();

            return Ok(aluno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlunoExists(string id)
        {
            return db.Aluno.Count(e => e.CPFAluno == id) > 0;
        }
    }
}