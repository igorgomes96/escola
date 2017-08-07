using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class DataNascimentoNaoInformadoException : AlunoImportacaoException
    {
        public DataNascimentoNaoInformadoException(int indexAluno) : base(indexAluno, "A data de nascimento do aluno não foi informado!")
        {
        }

        public DataNascimentoNaoInformadoException(int indexAluno, string mensagem) : base(indexAluno, mensagem)
        {
        }
    }
}