using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class EnderecoNaoInformadoException : AlunoImportacaoException
    {
        public EnderecoNaoInformadoException(int indexAluno) : base(indexAluno, "O endereço do aluno não foi informado!")
        {
        }

        public EnderecoNaoInformadoException(int indexAluno, string mensagem) : base(indexAluno, mensagem)
        {
        }
    }
}