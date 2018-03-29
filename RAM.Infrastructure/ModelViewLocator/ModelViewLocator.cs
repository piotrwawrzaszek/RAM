using RAM.Infrastructure.Startup;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Dialogs;
using RAM.Infrastructure.ViewModel.Menus;

namespace RAM.Infrastructure.ModelViewLocator
{
	public class ModelViewLocator
	{
		public IMainWindowViewModel MainWindowViewModel
			=> Container.Resolve<IMainWindowViewModel>();

		public IStatementGridViewModel StatementGridViewModel
			=> Container.Resolve<IStatementGridViewModel>();

		public IInputTapeViewModel InputTapeViewModel
			=> Container.Resolve<IInputTapeViewModel>();

		public IOutputTapeViewModel OutputTapeViewModel
			=> Container.Resolve<IOutputTapeViewModel>();

	    public IRegisterPanelViewModel RegisterPanelViewModel
	        => Container.Resolve<IRegisterPanelViewModel>();

        //Menus
	    public IMenuBarViewModel MenuBarViewModel
	        => Container.Resolve<IMenuBarViewModel>();

	    public IStatementContextMenuViewModel StatementContextMenuViewModel
	        => Container.Resolve<IStatementContextMenuViewModel>();

	    public IInputTapeContextMenuViewModel InputTapeContextMenuViewModel
	        => Container.Resolve<IInputTapeContextMenuViewModel>();

        //Dialogs
        public ILoadFileDialogViewModel LoadFileDialogViewModel
	        => Container.Resolve<ILoadFileDialogViewModel>();
	}
}