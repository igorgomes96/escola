using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class EntidadeNaoEncontradaException<TKey, TEntity> : Exception
    {
        public TKey Key { get; set; }
        public EntidadeNaoEncontradaException(TKey key) : base("Registro '" + typeof(TEntity).Name + "' não encontrado! (Chave: " + key + ")")
        {
            Key = key;
        }
    }
}