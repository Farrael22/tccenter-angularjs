using System;
using System.Collections.Generic;
using balcao.offline.api.DataAccess.Repository;
using balcao.offline.api.DTO;
using balcao.offline.api.Entity;
using Moq;

namespace balcao.offline.api.tests.Mocks
{
    internal class MockKitVirtualRepository
    {

        private Mock<IKitVirtualRepository> mock = new Mock<IKitVirtualRepository>();

        public static MockKitVirtualRepository Instancia()
        {
            return new MockKitVirtualRepository();
        }

        public Mock<IKitVirtualRepository> Mock()
        {
            return mock;
        }

        public MockKitVirtualRepository ListarProdutosKitRegra1()
        {
            #region Regra 1
            var produtoKit1 = new KitVirtualEntity
            {
                //CodigoKit = "KV00016729",
                //InicioVigencia = new DateTime(2018, 04, 02),
                //FimVigencia = new DateTime(2019, 08, 25),
                //TipoRegra = 1,
                //TipoAplicacao = 3,
                //TituloKit =
                //DescricaoKit =
                //ValorDesconto =
                //PercentualDesconto =
                //MaxPorCupom =
                //QtdMinProdutoA =
                //QtdMinProdutoB =
                //ValorIndenizacao =
                //CodigoProduto =
                //ListaProduto =
                //CustoNegociado =
                //IndicadorProdutoIdenizado =
                //PrecoPraticado =
                //PrecoPraticadoTemMaisAraujo =

            };
            #endregion

            var produtosKit = new List<KitVirtualEntity>();
            produtosKit.Add(produtoKit1);

            mock.Setup(mock => mock.ListarKitsVirtuais(It.IsAny<List<long>>()))
                .Returns(produtosKit);

            return this;
        }

    }
}
