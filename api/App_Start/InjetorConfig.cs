using tccenter.api.Business;
using tccenter.api.DataAccess.Repository;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using System;
using tccenter.api.DataAccess.Repository.Usuario;
using tccenter.api.DataAccess.Repository.TopicosInteressantes;
using tccenter.api.Business.Usuario;
using tccenter.api.Business.TopicosInteressantes;
using tccenter.api.DataAccess.Repository.InteressesUsuarios;

namespace IoC
{
    public static class InjetorConfig
    {
        public static void RegistrarContainer()
        {
            Injetor.Iniciar(container => 
            {
                //Repositorio
                container.Register<IUsuarioRepository, UsuarioRepository>();
                container.Register<ITopicosInteressantesRepository, TopicosInteressantesRepository>();
                container.Register<IInteressesUsuarios, InteressesUsuarios>();

                //Business
                container.Register<IUsuarioBusiness, UsuarioBusiness>();
                container.Register<ITopicosInteressantesBusiness, TopicosInteressantesBusiness>();
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