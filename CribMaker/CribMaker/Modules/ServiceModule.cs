using Autofac;
using CribMaker.Services.Services.Factory;

namespace CribMaker.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(ServiceManager)).As(typeof(IServiceManager)).InstancePerLifetimeScope();
        }
    }
}