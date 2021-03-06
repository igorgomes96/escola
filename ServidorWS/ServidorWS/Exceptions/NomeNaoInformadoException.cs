﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Exceptions
{
    public class NomeNaoInformadoException : AlunoImportacaoException
    {
        public string CPF { get; set; }
        public NomeNaoInformadoException(int indexAluno, string CPF) : base(indexAluno)
        {
            this.CPF = CPF;
        }

    }
}