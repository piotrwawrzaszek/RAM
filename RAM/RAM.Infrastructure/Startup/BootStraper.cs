using Autofac;
using Prism.Events;
using RAM.Infrastructure.Startup.Modules;

namespace RAM.Infrastructure.Startup
{
    public class BootStraper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterModule<ViewModelModule>();
            builder.RegisterModule<DataProviderModule>();
            builder.RegisterModule<ServiceModule>();

            return builder.Build();
        }
    }
}