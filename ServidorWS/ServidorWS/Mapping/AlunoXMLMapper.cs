using ServidorWS.Models;
using ServidorWS.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Mapping
{
    public class AlunoXMLMapper : IMapper<AlunoXML, Aluno>
    {
        public Aluno Map(AlunoXML source)
        {
            return new Aluno
            {
                CPFAluno = source.CPFAluno,
                NomeAluno = source.NomeAluno,
                DataNascimento = source.DataNascimento,
                NomeMae = source.NomeMae,
                Endereco = source.Endereco
            };
        }

        public AlunoXML Map(Aluno destination)
        {
            return new AlunoXML
            {
                CPFAluno = destination.CPFAluno,
                NomeAluno = destination.NomeAluno,
                DataNascimento = destination.DataNascimento,
                NomeMae = destination.NomeMae,
                Endereco = destination.Endereco
            };
        }

        public ICollection<Aluno> Map(ICollection<AlunoXML> source)
        {
            return source.Select(x => Map(x)).ToList();
        }

        public ICollection<AlunoXML> Map(ICollection<Aluno> destination)
        {
            return destination.Select(x => Map(x)).ToList();
        }
    }
}