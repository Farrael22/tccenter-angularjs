using System.Collections.Generic;

namespace balcao.offline.api.Helpers.Controllers
{
    /// <summary>
    /// Retorno de Erro
    /// </summary>
    public class RetornoErro
    {        
        /// <summary>
        /// Indica se o erro foi de negócio
        /// </summary>
        public bool ErroDeNegocio { get; set; } // false
        /// <summary>
        /// Codigo do Log do Erro
        /// </summary>
        public long? CodigoDoLog { get; set; }
        /// <summary>
        /// Lista de erros
        /// </summary>
        public string Mensagem { get; set; }
        /// <summary>
        /// Log
        /// </summary>        
    }

}