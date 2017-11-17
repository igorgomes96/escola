using ServidorWS.Dto;
using ServidorWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWS.Mapping
{
    public class ImportacaoMapper : IMapper<ImportacaoDto, Importacao>
    {
        public Importacao Map(ImportacaoDto source)
        {
            return new Importacao
            {
                Id = source.Id,
                NomeArquivo = source.NomeArquivo,
                DataHora = source.DataHora
            };
        }

        public ImportacaoDto Map(Importacao destination)
        {
            return new ImportacaoDto
            {
                Id = destination.Id,
                NomeArquivo = destination.NomeArquivo,
                DataHora = destination.DataHora
            };
        }

        public ICollection<Importacao> Map(ICollection<ImportacaoDto> source)
        {
            return source.Select(x => Map(x)).ToList();
        }

        public ICollection<ImportacaoDto> Map(ICollection<Importacao> destination)
        {
            return destination.Select(x => Map(x)).ToList();
        }
    }
}