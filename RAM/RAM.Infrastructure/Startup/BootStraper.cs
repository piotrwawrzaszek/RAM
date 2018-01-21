using Autofac;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.ViewModel;

namespace RAM.Infrastructure.Startup
{
	public class BootStraper
	{
		public IContainer BootStrap()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
			builder.RegisterType<MenuBarViewModel>().As<IMenuBarViewModel>();
			builder.RegisterType<MenuItemViewModel>().As<IMenuItemViewModel>();
			builder.RegisterType<StatementGridViewModel>().As<IStatementGridViewModel>();
			builder.RegisterType<InputTapeViewModel>().As<IInputTapeViewModel>();
			builder.RegisterType<OutputTapeViewModel>().As<IOutputTapeViewModel>();

			builder.RegisterType<StatementProvider>().As<IStatementProvider>();

			return builder.Build();
		}
	}
}