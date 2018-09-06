using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace balcao.offline.api.Helpers
{
    public static class MessageHelper
    {
        #region Mensagens de exceção
        public const string BANCO_INDISPONIVEL = "Banco de dados indisponível";
        public const string PRODUTOS_EQUIVALENTES_NAO_ENCONTRADOS = "Produtos equivalentes não encontrados";
        public const string PRODUTOS_NAO_ENCONTRADOS = "Produtos não encontrados";
        public const string KITS_NAO_ENCONTRADOS = "Kits Virtuais não encontrados";
        public const string ESTACAO_NAO_ENCONTRADA = "Estação não encontrada";
        public const string API_ONLINE_INDISPONIVELL = "Não foi possível comunicar com a api online";

        #endregion
    }
}