using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class CPFNaoInformadoException : AlunoImportacaoException
    {
        public CPFNaoInformadoException(int indexAluno) : base(indexAluno, "O CPF do aluno não foi informado!")
        {
        }

        public CPFNaoInformadoException(int indexAluno, string mensagem) : base(indexAluno, mensagem)
        {
        }
    }
}