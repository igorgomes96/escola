using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServidorWS.Models;
using ServidorWS.Repository;

namespace ServidorWS.Services
{
    public class AlunosService : IAlunosService
    {
        private readonly IGenericRepository<string, Aluno> _repository;

        public AlunosService(IGenericRepository<string, Aluno> alunosRepository)
        {
            _repository = alunosRepository;
        }


        public Aluno Delete(string cpf)
        {
            return _repository.Delete(cpf);
        }

        public bool Existe(string cpf)
        {
            return _repository.Existe(cpf);
        }

        public Aluno Find(string cpf)
        {
            return _repository.Find(cpf);
        }

        public ICollection<Aluno> ListAll()
        {
            return _repository.ListAll();
        }

        public void Save(Aluno aluno)
        {
            _repository.Save(aluno);
        }

        public void SaveAll(ICollection<Aluno> alunos)
        {
            _repository.SaveAll(alunos);
        }
    }
}