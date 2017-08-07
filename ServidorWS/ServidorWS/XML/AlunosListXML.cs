using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ServidorWS.XML
{
    [XmlRoot("AlunoList")]
    public class AlunoListXML
    {
        [XmlElement("Aluno")]
        public List<AlunoXML> Alunos { get; set; }
    }
}