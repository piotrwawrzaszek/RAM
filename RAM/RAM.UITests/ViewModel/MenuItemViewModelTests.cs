using System.Collections.ObjectModel;
using System.Windows.Input;
using FluentAssertions;
using Moq;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.ViewModel.Wrapper;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MenuItemViewModelTests
	{
		public MenuItemViewModelTests()
		{
			_parentMenuItemMock = new Mock<IMenuItemViewModel>();
			_menuItemViewModel = new MenuItemViewModel(_parentMenuItemMock.Object);
		}

		private readonly IMenuItemViewModel _menuItemViewModel;
		private readonly Mock<IMenuItemViewModel> _parentMenuItemMock;

		[Fact]
		public void Should_raise_property_changed_event_for_child_menu_items()
		{
			_menuItemViewModel.MonitorEvents();
			_menuItemViewModel.ChildMenuItems = new ObservableCollection<MenuItemViewModel>();
			_menuItemViewModel.ShouldRaisePropertyChangeFor(x => x.ChildMenuItems);
		}

		[Fact]
		public void Should_raise_property_changed_event_for_header()
		{
			_menuItemViewModel.MonitorEvents();
			_menuItemViewModel.Header = "Different header";
			_menuItemViewModel.ShouldRaisePropertyChangeFor(x => x.Header);
		}

		[Fact]
		public void Should_raise_property_changed_event_for_parent_menu_item()
		{
			_menuItemViewModel.MonitorEvents();
			_menuItemViewModel.ParentMenuItem = new MenuItemViewModel();
			_menuItemViewModel.ShouldRaisePropertyChangeFor(x => x.ParentMenuItem);
		}
	}
}
