using System.Collections.ObjectModel;
using FluentAssertions;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Wrapper;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MenuBarViewModeltests
	{
		public MenuBarViewModeltests()
		{
			_menuBarViewModel = new MenuBarViewModel();
		}

		private readonly IMenuBarViewModel _menuBarViewModel;

		[Fact]
		public void Should_raise_property_changed_event_for_menu_items()
		{
			_menuBarViewModel.MonitorEvents();
			_menuBarViewModel.MenuItems = new ObservableCollection<IMenuItemViewModel>();
			_menuBarViewModel.ShouldRaisePropertyChangeFor(x => x.MenuItems);
		}
	}
}