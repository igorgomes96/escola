using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Dto
{
    /// <summary>
    /// Classe com os dados da importação, mas sem o arquivo.
    /// Importante para listar as importações, mas sem enviar o arquivo junto, pois este pode gerar um overhead pela quantidade de dados.
    /// </summary>
    public class ImportacaoDto
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string NomeArquivo { get; set; }
    }
}