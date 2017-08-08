using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class EnderecoNaoInformadoException : AlunoImportacaoException
    {
        public string CPF { get; set; }
        public EnderecoNaoInformadoException(int indexAluno, string CPF) : base(indexAluno)
        {
            this.CPF = CPF;
        }

    }
}