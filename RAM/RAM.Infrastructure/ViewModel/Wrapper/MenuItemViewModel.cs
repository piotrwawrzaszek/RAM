using System.Collections.ObjectModel;
using System.Windows.Input;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel.Wrapper
{
	public interface IMenuItemViewModel : IViewModel
	{
		string Header { get; set; }
		IMenuItemViewModel ParentMenuItem { get; set; }
		ObservableCollection<MenuItemViewModel> ChildMenuItems { get; set; }
	}

	public class MenuItemViewModel : BaseViewModel, IMenuItemViewModel
	{
		private ObservableCollection<MenuItemViewModel> _childMenuItems;
		private string _header;
		private IMenuItemViewModel _parentMenuItem;

		public MenuItemViewModel(IMenuItemViewModel parentMenuItem = null,
			ICommand command = null)
		{
			ParentMenuItem = parentMenuItem;
			Command = command;
			_childMenuItems = new ObservableCollection<MenuItemViewModel>();
		}

		public ICommand Command { get; protected set; }

		public ObservableCollection<MenuItemViewModel> ChildMenuItems
		{
			get => _childMenuItems;
			set => SetProperty(ref _childMenuItems, value);
		}

		public string Header
		{
			get => _header;
			set => SetProperty(ref _header, value);
		}

		public IMenuItemViewModel ParentMenuItem
		{
			get => _parentMenuItem;
			set => SetProperty(ref _parentMenuItem, value);
		}
	}
}