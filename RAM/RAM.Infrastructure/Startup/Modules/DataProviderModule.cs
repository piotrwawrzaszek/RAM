using System.Reflection;
using Autofac;
using RAM.Infrastructure.Data;
using Module = Autofac.Module;

namespace RAM.Infrastructure.Startup.Modules
{
    public class DataProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(DataProviderModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IDataProvider>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}