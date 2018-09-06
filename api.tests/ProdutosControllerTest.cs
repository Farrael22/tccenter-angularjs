using balcao.offline.api.Business;
using balcao.offline.api.tests.Mocks;
using balcao.offline.api.Helpers.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace balcao.offline.api.tests
{
    [TestClass]
    public class ProdutosControllerTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            AutoMapperConfig.RegisterMappings();
        }
        [TestCleanup]
        public void TestCleanup()
        {
            AutoMapperConfig.ResetMappings();
        }

        [TestMethod]
        public void Pesquisa_Produtos()
        {
            var produtosRepositorio = MockProdutosRepository.Instancia()
                .ListarProdutos()
                .Mock();
            var produtosNegocio = new ProdutosBusiness(produtosRepositorio.Object);
            //var result = produtosNegocio.ObterProdutos(1, "");

            //Assert.IsNotNull(result);
            //Assert.IsTrue(result.ToList().Count > 0);
        }
    }
}
