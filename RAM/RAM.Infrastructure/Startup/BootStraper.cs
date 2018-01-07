using Autofac;
using RAM.Infrastructure.ViewModel;

namespace RAM.Infrastructure.Startup
{
	public class BootStraper
	{
		public IContainer BootStrap()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();

			return builder.Build();
		}
	}
}