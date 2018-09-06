using System;
using System.Collections.Generic;

namespace balcao.offline.api.Entity
{
    public class ProdutoEntity
    {
        public long CodigoInterno { get; set; }
        public int Codigo { get; set; }
        public int Digito { get; set; }
        public string DescricaoResumida { get; set; }
        public string DescricaoCompleta { get; set; }
        public decimal PrecoDe { get; set; }
        public decimal PrecoPor { get; set; }
        //public Situacao Situacao { get; set; }
        //public decimal PercentualDesconto { get; set; } // possivel campo calculado   
        //public decimal Economia { get; set; } // possivel campo calculado             

        private decimal _PrecoFP;
        public decimal PrecoFP
        {
            get
            {
                if (_PrecoFP < 0)
                    return 0;
                return _PrecoFP;
            }
            set
            {
                _PrecoFP = value;
            }
        }

        public string Fabricante { get; set; }
        public string TipoReceita { get; set; }
        public string TipoEmbalagem { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        //public string Segmento { get; set; }                                          
        //public string SubSegmento { get; set; }                                       
        public int PrioridadeVenda { get; set; }
        //public int EstoqueNaFilial { get; set; }                                      

        //public bool IndicPopup { get; set; }                                          
        public bool IndicUsoContinuo { get; set; }
        public bool IndicGeladeira { get; set; }
        public bool IndicExclusivoFP { get; set; }

        //public bool IndicGBM { get; set; }                                            
        public bool IndicPBM { get; set; }
        public bool IndicFP { get; set; }
        public bool IndicREC { get; set; }
        public bool IndicAntibiotico { get; set; }
        public bool IndicPsicotropico { get; set; }
        //public bool IndicAnticoncepcional { get; set; }
        public int QuantidadeEmbalagem { get; set; }
        public int QuantidadeEmbalagemFP { get; set; }
        public int QuantidadeDiasFP { get; set; }
        public int QuantidadeUnidadesFP { get; set; }
        public int Ordenacao { get; set; }

        public string Classificacao { get; set; }
        //public List<Tag> Tags { get; set; }
        //public string Tag { get; set;}

        public string PrincipioAtivo { get; set; }

        public int PeriodoTratamento { get; set; }
        //public string InformacaoGecom { get; set; }
        public List<string> CodigosBarra { get; set; }

        // Exemplo de retornar a description de um enum

        //public string SituacaoDesc { get { return Situacao.GetDescription(); } }

        public bool IndicIntercambiavel { get; set; }
        //public double PercentualDescontoCRM { get; set; }
        public double ValorPrecoCRM { get; set; }
        public bool KitExclusivoCRM { get; set; }
        public string DinamicaKit { get; set; }
        public string TipoRegraKit { get; set; }

        public DateTime DataAtualizacao { get; set; }

    }
}