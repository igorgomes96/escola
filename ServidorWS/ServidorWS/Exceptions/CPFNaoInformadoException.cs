using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class CPFNaoInformadoException : AlunoImportacaoException
    {
        public CPFNaoInformadoException(int indexAluno) : base(indexAluno)
        {
        }

    }
}