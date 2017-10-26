using Autofac;
using CribMaker.Core.Repositories.Factory;

namespace CribMaker.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(RepositoryManager)).As(typeof(IRepositoryManager)).InstancePerLifetimeScope();
        }
    }
}