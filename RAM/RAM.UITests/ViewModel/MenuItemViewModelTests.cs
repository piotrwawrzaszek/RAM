using System.Collections.ObjectModel;
using System.Windows.Input;
using FluentAssertions;
using Moq;
using Prism.Events;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.ViewModel;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MenuItemViewModelTests
	{
		public MenuItemViewModelTests()
		{
		    _eventAggregatorMock = new Mock<IEventAggregator>();

		    var languageChangedEventMock = new Mock<LanguageChangedEvent>();
		    _eventAggregatorMock
		        .Setup(ea => ea.GetEvent<LanguageChangedEvent>())
		        .Returns(languageChangedEventMock.Object);

            _commandMock = new Mock<ICommand>();
		    _menuItemViewModel = new MenuItemViewModel(() => "Header",
                _eventAggregatorMock.Object).Load(_commandMock.Object);

		}

		private readonly IMenuItemViewModel _menuItemViewModel;
	    private readonly Mock<IEventAggregator> _eventAggregatorMock;
	    private readonly Mock<ICommand> _commandMock;

        [Fact]
		public void Should_raise_property_changed_event_for_child_menu_items()
		{
			_menuItemViewModel.MonitorEvents();
			_menuItemViewModel.Children = new ObservableCollection<IMenuItemViewModel>();
			_menuItemViewModel.ShouldRaisePropertyChangeFor(x => x.Children);
		}

		[Fact]
		public void Should_raise_property_changed_event_for_header()
		{
			_menuItemViewModel.Model.MonitorEvents();
			_menuItemViewModel.Model.Header = "Different header";
			_menuItemViewModel.Model.ShouldRaisePropertyChangeFor(x => x.Model.Header);
		}

		[Fact]
		public void Should_raise_property_changed_event_for_is_checked()
		{
			_menuItemViewModel.Model.MonitorEvents();
			_menuItemViewModel.Model.IsChecked = true;
			_menuItemViewModel.Model.ShouldRaisePropertyChangeFor(x => x.Model.IsChecked);
		}
	}
}