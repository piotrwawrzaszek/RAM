using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FluentAssertions;
using Moq;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Wrapper;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MenuBarViewModeltests
	{
		public MenuBarViewModeltests()
		{
			_menuItemViewModelMock = new Mock<IMenuItemViewModel>();
			_menuBarViewModel = new MenuBarViewModel(CreateMenuItemViewModel);
		}

		private readonly Mock<IMenuItemViewModel> _menuItemViewModelMock;
		private readonly IMenuBarViewModel _menuBarViewModel;

		private static IMenuItemViewModel CreateMenuItemViewModel()
		{
			var menuItemViewModelMock = new Mock<IMenuItemViewModel>();
			
			menuItemViewModelMock.Setup(vm =>
					vm.Load(It.IsAny<MenuItemWrapper>(),
						It.IsAny<ICommand>(),
						It.IsAny<IEnumerable<IMenuItemViewModel>>()))
				.Callback<MenuItemWrapper, ICommand, IEnumerable<IMenuItemViewModel>>((x, y, z) =>
				{
					menuItemViewModelMock.Setup(vm => vm.Model).Returns(x);
					menuItemViewModelMock.Setup(vm => vm.Command).Returns(y);
					menuItemViewModelMock.Setup(vm => vm.Children).Returns(new ObservableCollection<IMenuItemViewModel>(z));
				});
			return menuItemViewModelMock.Object;
		}

		[Fact]
		public void Should_raise_property_changed_event_for_menu_items()
		{
			_menuBarViewModel.MonitorEvents();
			_menuBarViewModel.MenuItems = new ObservableCollection<IMenuItemViewModel>();
			_menuBarViewModel.ShouldRaisePropertyChangeFor(x => x.MenuItems);
		}
	}
}