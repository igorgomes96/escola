using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class AlunoNaoEncontradoException : Exception
    {
        public string CPF { get; set; }
        public AlunoNaoEncontradoException(string cpf) : base("CPF " + cpf + " não encontrado!")
        {
            CPF = cpf;
        }
    }
}