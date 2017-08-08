using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class AlunoImportacaoException : Exception
    {
        public int IndexAluno { get; set; }
        public AlunoImportacaoException(int indexAluno)
        {
            IndexAluno = indexAluno;
        }
    }
}