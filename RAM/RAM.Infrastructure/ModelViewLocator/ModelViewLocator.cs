using RAM.Infrastructure.Startup;
using RAM.Infrastructure.ViewModel;

namespace RAM.Infrastructure.ModelViewLocator
{
	public class ModelViewLocator
	{
		public IMainWindowViewModel MainWindowViewModel
			=> Container.Resolve<IMainWindowViewModel>();
	}
}