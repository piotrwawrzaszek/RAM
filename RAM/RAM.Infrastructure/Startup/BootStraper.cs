using Autofac;
using RAM.Infrastructure.Startup.Modules;

namespace RAM.Infrastructure.Startup
{
    public class BootStraper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ViewModelModule>();
            builder.RegisterModule<DataProviderModule>();
            builder.RegisterModule<ServiceModule>();

            return builder.Build();
        }
    }
}