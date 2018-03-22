using RAM.Infrastructure.Startup;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Dialogs;

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

		public IStatementGridViewModel StatementGridViewModel
			=> Container.Resolve<IStatementGridViewModel>();

		public IInputTapeViewModel InputTapeViewModel
			=> Container.Resolve<IInputTapeViewModel>();

		public IOutputTapeViewModel OutputTapeViewModel
			=> Container.Resolve<IOutputTapeViewModel>();

	    public IRegisterPanelViewModel RegisterPanelViewModel
	        => Container.Resolve<IRegisterPanelViewModel>();

        //Dialogs
	    public ILoadFileDialogViewModel LoadFileDialogViewModel
	        => Container.Resolve<ILoadFileDialogViewModel>();
	}
}