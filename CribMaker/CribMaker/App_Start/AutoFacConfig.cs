using Autofac;
using System.Web.Mvc;
using CribMaker.Modules;
using Autofac.Integration.Mvc;

namespace CribMaker
{
    public static class AutoFacConfig
    {
        public static void RegisterModules()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EfModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}