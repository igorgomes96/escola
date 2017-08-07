using ServidorWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ServidorWS.XML
{
    public class AlunoXML
    {
        public AlunoXML() { }
        public AlunoXML(Aluno aluno)
        {
            CPFAluno = aluno.CPFAluno;
            NomeAluno = aluno.NomeAluno;
            DataNascimento = aluno.DataNascimento;
            NomeMae = aluno.NomeMae;
            Endereco = aluno.Endereco;
        }

        [XmlElement]
        public string CPFAluno { get; set; }
        [XmlElement]
        public string NomeAluno { get; set; }
        [XmlElement(DataType = "date")]
        public System.DateTime DataNascimento { get; set; }
        [XmlElement]
        public string NomeMae { get; set; }
        [XmlElement]
        public string Endereco { get; set; }

        public Aluno GetAlunoModel()
        {
            return new Aluno
            {
                CPFAluno = CPFAluno,
                NomeAluno = NomeAluno,
                DataNascimento = DataNascimento,
                NomeMae = NomeMae,
                Endereco = Endereco
            };
        }
    }
}