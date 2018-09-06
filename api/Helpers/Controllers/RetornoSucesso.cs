using System.Collections.Generic;
using System.Linq;

namespace balcao.offline.api.Helpers.Controllers
{
    /// <summary>
    /// Retorno padrao para objeto
    /// </summary>
    public class Result<T>
    {
        /// <summary>
        /// Registro
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// Retorno padrao para listas de objetos
    /// </summary>
    public class ResultList<T>
    {
        /// <summary>
        /// Registros 
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Total de registros retornados
        /// </summary>
        public int Count { get { return Data?.Count() ?? 0; } }
    }
}
