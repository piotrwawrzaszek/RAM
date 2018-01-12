using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
	public interface IMenuItemViewModel : IViewModel
	{
		MenuItemWrapper Model { get; set; }
		ICommand Command { get; }
		ObservableCollection<IMenuItemViewModel> Children { get; set; }

		void Load(MenuItemWrapper menuItemWrapper, ICommand command,
			IEnumerable<IMenuItemViewModel> children);
	}

	public class MenuItemViewModel : BaseViewModel, IMenuItemViewModel
	{
		private ObservableCollection<IMenuItemViewModel> _childMenuItems;
		private MenuItemWrapper _model;

		public MenuItemViewModel()
		{
			_childMenuItems = new ObservableCollection<IMenuItemViewModel>();
		}

		public ICommand Command { get; protected set; }

		public ObservableCollection<IMenuItemViewModel> Children
		{
			get => _childMenuItems;
			set => SetProperty(ref _childMenuItems, value);
		}

		public MenuItemWrapper Model
		{
			get => _model;
			set => SetProperty(ref _model, value);
		}

		public void Load(MenuItemWrapper menuItemWrapper, ICommand command,
			IEnumerable<IMenuItemViewModel> children)
		{
			Model = menuItemWrapper;
			Command = command;
			Children = new ObservableCollection<IMenuItemViewModel>(children);
		}
	}
}