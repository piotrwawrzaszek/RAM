using System.Reflection;
using Autofac;
using RAM.Infrastructure.ViewModel.Base;
using Module = Autofac.Module;

namespace RAM.Infrastructure.Startup.Modules
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ViewModelModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IViewModel>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}