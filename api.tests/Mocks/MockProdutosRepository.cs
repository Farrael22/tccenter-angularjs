using System;
using System.Collections.Generic;
using balcao.offline.api.DataAccess.Repository;
using balcao.offline.api.DTO;
using balcao.offline.api.Entity;
using Moq;

namespace balcao.offline.api.tests.Mocks
{
    internal class MockProdutosRepository
    {

        private Mock<IProdutosRepository> mock = new Mock<IProdutosRepository>();

        public static MockProdutosRepository Instancia()
        {
            return new MockProdutosRepository();
        }

        public Mock<IProdutosRepository> Mock()
        {
            return mock;
        }

        public MockProdutosRepository ListarProdutos()
        {
            var produto1 = new ProdutoEntity
            {
                Codigo = 1780,
                Digito = 9
            };

            var produto2 = new ProdutoEntity
            {
                Codigo = 79953,
                Digito = 0
            };

            List<ProdutoEntity> produtos = new List<ProdutoEntity>();
            produtos.Add(produto1);
            produtos.Add(produto2);

            //mock.Setup(mock => mock.ListarProdutos(It.IsAny<int>(), It.IsAny<string>()))
            //    .Returns(produtos);

            return this;
        }

    }
}
