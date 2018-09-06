using balcao.offline.api.Helpers.Business;
using balcao.offline.api.Business;
using balcao.offline.api.tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace balcao.offline.api.tests
{
    [TestClass]
    public class KitVirtualControllerTest
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
        public void Pesquisa_Kits_Virtuais()
        {
            var kitRepositorio = MockKitVirtualRepository.Instancia()
                .ListarProdutosKitRegra1()
                .Mock();
            var kitsNegocio = new KitVirtualBusiness(kitRepositorio.Object);
            var result = kitsNegocio.ObterKitsVirtuais(new List<DTO.KitVirtualDTO.Produto>());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ToList().Count > 0);
        }
    }
}
