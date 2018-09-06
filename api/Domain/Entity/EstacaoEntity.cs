using System;

namespace balcao.offline.api.Entity
{
    public class EstacaoEntity
    {
        public int Estacao { get; set; }
        public string NomeEstacao { get; set; }
        public int Filial { get; set; }
        public string NomeFilial { get; set; }
        public string Ip { get; set; }
        public int Situacao { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}