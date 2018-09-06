using balcao.offline.api.Business;
using balcao.offline.api.DataAccess.Repository;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using System;

namespace IoC
{
    public static class InjetorConfig
    {
        public static void RegistrarContainer()
        {
            Injetor.Iniciar(container => 
            {
                //Repositorio
                container.Register<IProdutosRepository, ProdutosRepository>();
                container.Register<IEstacaoRepository, EstacaoRepository>();
                container.Register<IKitVirtualRepository, KitVirtualRepository>();

                //Business
                container.Register<IProdutosBusiness, ProdutosBusiness>();
                container.Register<IEstacaoBusiness, EstacaoBusiness>();
                container.Register<IKitVirtualBusiness, KitVirtualBusiness>();
            });
        }
    }

    public static class Injetor
    {
        private static Container _container = new Container();

        public static Container GetContainer
        {
            get
            {
                return _container;
            }
        }

        public static void Iniciar(Action<Container> config)
        {
            var container = GetContainer;
            config(container);
            IniciarContainer(container);
        }

        private static void IniciarContainer(Container container)
        {
            // Concluindo
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration); //web api          
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container); //web api
        }
    }
}