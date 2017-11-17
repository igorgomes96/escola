using ServidorWS.Models;
using ServidorWS.Exceptions;
using System.Collections.Generic;
using System;

namespace ServidorWS.Repository
{
    public interface IGenericRepository<TKey, TEntity> where TEntity : class
    {
        /// <summary>
        /// Lista todas as entidades
        /// </summary>
        /// <returns>Coleção com todos as entidades</returns>
        ICollection<TEntity> ListAll();

        /// <summary>
        /// Faz uma consulta no banco
        /// </summary>
        /// <param name="predicate">Query</param>
        /// <returns>Coleção com todos as entidades retornadas pela consulta</returns>
        ICollection<TEntity> Query(Func<TEntity, bool> predicate);

        /// <summary>
        /// Busca uma entidade pela chave
        /// </summary>
        /// <param name="key">Chave</param>
        /// <returns>Entidade</returns>
        /// <exception cref="EntidadeNaoEncontradaException{TKey, TEntity}">Registro não encontrado</exception>
        TEntity Find(TKey key);

        /// <summary>
        /// Insere ou atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade a ser salva</param>
        void Save(TEntity entity);

        /// <summary>
        /// Insere ou atualiza todas as entidades.
        /// </summary>
        /// <param name="entities">Entidades a serem salvas</param>
        void SaveAll(ICollection<TEntity> entities);

        /// <summary>
        /// Exclui a entidade
        /// </summary>
        /// <param name="key">Chave da entidade a ser excluída</param>
        /// <returns>Entidade excluída</returns>
        /// <exception cref="EntidadeNaoEncontradaException{TKey, TEntity}">Entidade não encontrada</exception>
        TEntity Delete(TKey key);

        /// <summary>
        /// Verifica se já existe a entidade
        /// </summary>
        /// <param name="key">Chave da entidade</param>
        /// <returns>true, se existe; false, se não existe</returns>
        bool Existe(TKey key);
    }
}
