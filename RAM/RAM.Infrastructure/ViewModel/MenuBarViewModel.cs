using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;
using MenuItem = RAM.Domain.Model.MenuItem;

namespace RAM.Infrastructure.ViewModel
{
	public interface IMenuBarViewModel : IViewModel
	{
		ObservableCollection<IMenuItemViewModel> MenuItems { get; set; }
	}

	public class MenuBarViewModel : BaseViewModel, IMenuBarViewModel
	{
		private readonly Func<IMenuItemViewModel> _menuItemViewModelCreator;

		private ObservableCollection<IMenuItemViewModel> _menuItems;

		public MenuBarViewModel(Func<IMenuItemViewModel> menuItemViewModelCreator)
		{
			_menuItemViewModelCreator = menuItemViewModelCreator;

			MenuItems = Seed();
		}

		public ObservableCollection<IMenuItemViewModel> MenuItems
		{
			get => _menuItems;
			set => SetProperty(ref _menuItems, value);
		}

		#region Handler methods

		private void Execute(object o)
		{
			// Only for current testing purpose
			var result = MessageBox.Show(@"Do you want to close this window?", @"Confirmation", MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
				Application.Exit();
		}

		#endregion

		#region Menu items creation

		private ObservableCollection<IMenuItemViewModel> Seed()
		{
			var menuItems = new ObservableCollection<IMenuItemViewModel>
			{
				CreateMenuItem(Resources.MenuItems.FileEN, null,
					new List<IMenuItemViewModel>
					{
						CreateMenuItem("Option 1", new RelayCommand(Execute), new List<IMenuItemViewModel>()),
						CreateMenuItem("Option 2", new RelayCommand(Execute), new List<IMenuItemViewModel>())
					}),
				CreateMenuItem(Resources.MenuItems.EditEN, null,
					new List<IMenuItemViewModel>
					{
						CreateMenuItem("Option 1", new RelayCommand(Execute), new List<IMenuItemViewModel>()),
						CreateMenuItem("Option 2", new RelayCommand(Execute), new List<IMenuItemViewModel>())
					}),
				CreateMenuItem(Resources.MenuItems.ViewEN, null,
					new List<IMenuItemViewModel>
					{
						CreateMenuItem("Option 1", new RelayCommand(Execute), new List<IMenuItemViewModel>()),
						CreateMenuItem("Option 2", new RelayCommand(Execute), new List<IMenuItemViewModel>())
					}),
				CreateMenuItem(Resources.MenuItems.AboutEN, null,
					new List<IMenuItemViewModel>
					{
						CreateMenuItem("Option 1", new RelayCommand(Execute), new List<IMenuItemViewModel>()),
						CreateMenuItem("Option 2", new RelayCommand(Execute), new List<IMenuItemViewModel>())
					})
			};
			return menuItems;
		}

		private IMenuItemViewModel CreateMenuItem(string header,
			ICommand command, IEnumerable<IMenuItemViewModel> children)
		{
			var menuItem = _menuItemViewModelCreator();

			menuItem.Load(new MenuItemWrapper(new MenuItem(header)), command, children);

			return menuItem;
		}

		#endregion
	}
}