using ServidorWS.Models;
using ServidorWS.Exceptions;
using System.Collections.Generic;

namespace ServidorWS.Repository
{
    public interface IAlunosRepository
    {
        /// <summary>
        /// Lista todos os alunos.
        /// </summary>
        /// <returns>Coleção com todos os alunos.</returns>
        ICollection<Aluno> ListAll();

        /// <summary>
        /// Busca o registro do Aluno pelo CPF.
        /// </summary>
        /// <param name="cpf">CPF do aluno procurado</param>
        /// <returns>Objeto Aluno</returns>
        /// <exception cref="AlunoNaoEncontradoException">Não foi encontrado nenhum aluno com o CPF informado.</exception>
        Aluno Find(string cpf);

        /// <summary>
        /// Insere ou atualiza o aluno no banco.
        /// </summary>
        /// <param name="aluno">Objeto Aluno a ser salvo.</param>
        void Save(Aluno aluno);

        /// <summary>
        /// Insere ou atualiza todos os alunos.
        /// </summary>
        /// <param name="alunos">Alunos a serem salvos</param>
        void SaveAll(ICollection<Aluno> alunos);

        /// <summary>
        /// Exclui o aluno do banco.
        /// </summary>
        /// <param name="cpf">CPF do aluno a ser excluído</param>
        /// <returns>Aluno excluído</returns>
        /// <exception cref="AlunoNaoEncontradoException">Não foi encontrado nenhum aluno com o CPF informado</exception>
        Aluno Delete(string cpf);

        /// <summary>
        /// Verifica se já existe o aluno cadastrado no banco.
        /// </summary>
        /// <param name="cpf">CPF do aluno</param>
        /// <returns>true, se existe; false, se não existe</returns>
        bool Existe(string cpf);
    }
}
