using System.Collections.ObjectModel;
using FluentAssertions;
using Moq;
using Prism.Events;
using RAM.Domain.Model;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Wrapper;
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

            _menuItemViewModel = new MenuItemViewModel(_eventAggregatorMock.Object)
			{
				Model = new MenuItemWrapper(new MenuItem("Header"))
			};
		}

		private readonly IMenuItemViewModel _menuItemViewModel;
	    private readonly Mock<IEventAggregator> _eventAggregatorMock;

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