using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class NomeNaoInformadoException : AlunoImportacaoException
    {
        public NomeNaoInformadoException(int indexAluno) : base(indexAluno, "O nome do Aluno não foi informado!")
        {
        }

        public NomeNaoInformadoException(int indexAluno, string mensagem) : base(indexAluno, mensagem)
        {
        }
    }
}