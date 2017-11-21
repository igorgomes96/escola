using ServidorWS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class EntidadeNaoEncontradaException<TKey, TEntity> : Exception
    {
        public TKey Key { get; set; }
        public EntidadeNaoEncontradaException(TKey key) : base(string.Format(MessagesResource.EntidadeNaoEncontrada, typeof(TEntity).Name, key))
        {
            Key = key;
        }
    }
}