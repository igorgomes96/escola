using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWS.Models;
using System.Data.Entity;
using ServidorWS.Exceptions;
using System.Data.Entity.Migrations;

namespace ServidorWS.Repository
{
    public class AlunosRepository : IAlunosRepository
    {
        private readonly DbContext _db;

        public AlunosRepository(DbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public Aluno Delete(string cpf)
        {
            Aluno aluno = _db.Set<Aluno>().Remove(Find(cpf));
            _db.SaveChanges();
            return aluno;
        }

        public bool Existe(string cpf)
        {
            return _db.Set<Aluno>().Any(x => x.CPFAluno == cpf);
        }

        public Aluno Find(string cpf)
        {
            Aluno aluno = _db.Set<Aluno>().Find(cpf);
            return aluno ?? throw new AlunoNaoEncontradoException(cpf);
        }

        public ICollection<Aluno> ListAll()
        {
            return _db.Set<Aluno>().ToList();
        }

        public void Save(Aluno aluno)
        {
            _db.Set<Aluno>().AddOrUpdate(aluno);
            _db.SaveChanges();
        }

        public void SaveAll(ICollection<Aluno> alunos)
        {
            alunos.ToList().ForEach(aluno =>
            {
                _db.Set<Aluno>().AddOrUpdate(aluno);
            });
            _db.SaveChanges();
        }
    }
}