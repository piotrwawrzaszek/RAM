using RAM.Infrastructure.Startup;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ModelViewLocator
{
	public class ModelViewLocator
	{
		public IMainWindowViewModel MainWindowViewModel
			=> Container.Resolve<IMainWindowViewModel>();

		public IMenuBarViewModel MenuBarViewModel
			=> Container.Resolve<IMenuBarViewModel>();

		public IMenuItemViewModel MenuItemViewModel 
			=> Container.Resolve<IMenuItemViewModel>();
	}
}