using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorWS.Mapping
{
    public interface IMapper<TSource, TDestination> where TDestination: class where TSource: class
    {
        /// <summary>
        /// Faz o mapeamento do objeto Source para o Destination
        /// </summary>
        /// <param name="source">Objeto a ser mapeado</param>
        /// <returns>Objeto mapeado</returns>
        TDestination Map(TSource source);

        /// <summary>
        /// Faz o mapeamento Inverso (Destination para Source)
        /// </summary>
        /// <param name="destination">Objeto a ser mapeado</param>
        /// <returns>Objeto mapeado</returns>
        TSource Map(TDestination destination);

        /// <summary>
        /// Faz o mapeamento de uma collection
        /// </summary>
        /// <param name="source">Collection a ser mapeada</param>
        /// <returns>Collection mapeada</returns>
        ICollection<TDestination> Map(ICollection<TSource> source);

        /// <summary>
        /// Faz o mapeamento inverso de uma collection
        /// </summary>
        /// <param name="destination">Collection a ser mapeada</param>
        /// <returns>Collection mapeada</returns>
        ICollection<TSource> Map(ICollection<TDestination> destination);
    }
}
