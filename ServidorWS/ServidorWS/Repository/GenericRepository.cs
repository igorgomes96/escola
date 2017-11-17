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
    public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity> where TEntity: class
    {
        private readonly DbContext _db;

        public GenericRepository(DbContext db)
        {
            _db = db; //?? throw new ArgumentNullException(nameof(db));
        }

        public TEntity Delete(TKey key)
        {
            TEntity entity = _db.Set<TEntity>().Remove(Find(key));
            _db.SaveChanges();
            return entity;
        }

        public bool Existe(string cpf)
        {
            return _db.Set<Aluno>().Any(x => x.CPFAluno == cpf);
        }

        public bool Existe(TKey key)
        {
            return _db.Set<Aluno>().Find(key) != null;
        }

        public TEntity Find(TKey key)
        {
            TEntity entity = _db.Set<TEntity>().Find(key);
            if (entity == null)
                throw new EntidadeNaoEncontradaException<TKey, TEntity>(key);
            return entity;
        }

        public ICollection<TEntity> Query(Func<TEntity, bool> predicate)
        {
            return _db.Set<TEntity>().Where(predicate).ToList();
        }

        public void Save(TEntity entity)
        {
            _db.Set<TEntity>().AddOrUpdate(entity);
            _db.SaveChanges();
        }


        public void SaveAll(ICollection<TEntity> entities)
        {
            entities.ToList().ForEach(entity =>
            {
                _db.Set<TEntity>().AddOrUpdate(entity);
            });
            _db.SaveChanges();
        }

        ICollection<TEntity> IGenericRepository<TKey, TEntity>.ListAll()
        {
            return _db.Set<TEntity>().ToList();
        }
    }
}