using Repository;
using Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Boss.WEBAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ITokenServices, TokenServices>();
            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

        }
    }
}