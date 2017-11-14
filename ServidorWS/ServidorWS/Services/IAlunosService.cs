using ServidorWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorWS.Services
{
    public interface IAlunosService
    {

        /// <summary>
        /// Lista todos os alunos.
        /// </summary>
        /// <returns>Coleção com todos os alunos.</returns>
        ICollection<Aluno> ListAll();

        /// <summary>
        /// Busca um aluno.
        /// </summary>
        /// <param name="cpf">CPF do aluno procurado</param>
        /// <returns>Objeto Aluno</returns>
        /// <exception cref="AlunoNaoEncontradoException">Não foi encontrado nenhum aluno com o CPF informado.</exception>
        Aluno Find(string cpf);

        /// <summary>
        /// Salva o aluno.
        /// </summary>
        /// <param name="aluno">Objeto Aluno a ser salvo.</param>
        void Save(Aluno aluno);

        /// <summary>
        /// Exclui o aluno.
        /// </summary>
        /// <param name="cpf">CPF do aluno a ser excluído</param>
        /// <returns>Aluno excluído</returns>
        /// <exception cref="AlunoNaoEncontradoException">Não foi encontrado nenhum aluno com o CPF informado</exception>
        Aluno Delete(string cpf);

        /// <summary>
        /// Verifica se já existe o aluno cadastrado.
        /// </summary>
        /// <param name="cpf">CPF do aluno</param>
        /// <returns>true, se existe; false, se não existe</returns>
        bool Existe(string cpf);
    }
}
