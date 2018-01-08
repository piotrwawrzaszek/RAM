using System.Collections.ObjectModel;
using System.Windows.Forms;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
	public interface IMenuBarViewModel : IViewModel
	{
		ObservableCollection<IMenuItemViewModel> MenuItems { get; set; }
	}

	public class MenuBarViewModel : BaseViewModel, IMenuBarViewModel
	{
		private ObservableCollection<IMenuItemViewModel> _menuItems;

		public MenuBarViewModel()
		{
			// TODO rebuild constructor

			_menuItems = new ObservableCollection<IMenuItemViewModel>
			{
				new MenuItemViewModel {Header = "File"},
				new MenuItemViewModel {Header = "Edit"},
				new MenuItemViewModel {Header = "View"},
				new MenuItemViewModel {Header = "About"}
			};

			var parent = _menuItems[0];
			parent.ChildMenuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(parent, new RelayCommand(Execute)) {Header = "Option 1"},
				new MenuItemViewModel(parent, new RelayCommand(Execute)) {Header = "Option 2"}
			};

			parent = _menuItems[1];
			parent.ChildMenuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(parent, new RelayCommand(Execute)) {Header = "Option 1"},
				new MenuItemViewModel(parent, new RelayCommand(Execute)) {Header = "Option 2"}
			};

			parent = _menuItems[2];
			parent.ChildMenuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(parent, new RelayCommand(Execute)) {Header = "Option 1"},
				new MenuItemViewModel(parent, new RelayCommand(Execute)) {Header = "Option 2"}
			};

			parent = _menuItems[3];
			parent.ChildMenuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(parent, new RelayCommand(Execute)) {Header = "Option 1"},
				new MenuItemViewModel(parent, new RelayCommand(Execute)) {Header = "Option 2"}
			};
		}

		public ObservableCollection<IMenuItemViewModel> MenuItems
		{
			get => _menuItems;
			set => SetProperty(ref _menuItems, value);
		}

		private void Execute(object o)
		{
			// Only for current testing purpose
			var result = MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
				Application.Exit();
		}
	}
}

/*
 
	public interface IMenuBarViewModel : IViewModel
	{
		ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
	}

	public class MenuBarBarViewModel : ViewModelBase, IMenuBarViewModel
	{
		private readonly IMenuItemProvider _menuItemProvider;
		private ObservableCollection<MenuItemViewModel> _menuItems;

		public MenuBarBarViewModel(IMenuItemProvider menuItemProvider)
		{
			_menuItemProvider = menuItemProvider;

			_menuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(null) {Header = "File"},
				new MenuItemViewModel(null) {Header = "Edit"},
				new MenuItemViewModel(null) {Header = "View"},
				new MenuItemViewModel(null) {Header = "About"}
			};

			var parent = _menuItems[0];
			parent.ChildMenuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(parent) {Header = "Option 1", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 2", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 3", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 4", Command = new RelayCommand(Execute)}
			};

			parent = _menuItems[1];
			parent.ChildMenuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(parent) {Header = "Option 1", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 2", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 3", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 4", Command = new RelayCommand(Execute)}
			};

			parent = _menuItems[2];
			parent.ChildMenuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(parent) {Header = "Option 1", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 2", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 3", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 4", Command = new RelayCommand(Execute)}
			};

			parent = _menuItems[3];
			parent.ChildMenuItems = new ObservableCollection<MenuItemViewModel>
			{
				new MenuItemViewModel(parent) {Header = "Option 1", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 2", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 3", Command = new RelayCommand(Execute)},
				new MenuItemViewModel(parent) {Header = "Option 4", Command = new RelayCommand(Execute)}
			};
		}

		public ObservableCollection<MenuItemViewModel> MenuItems
		{
			get => _menuItems;
			set
			{
				if (_menuItems == value) return;
				_menuItems = value;
				OnPropertyChanged();
			}
		}

		private void Execute(object o)
		{
			var result = MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo,
				MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
				Application.Current.Shutdown();
		}

		//public void Load()
		//{
		//	MenuItems.Clear();

		//	foreach (var item in _menuItemProvider.GetAllMenuItems())
		//		MenuItems.Add(item);
		//}
	}
}
	 
	 */