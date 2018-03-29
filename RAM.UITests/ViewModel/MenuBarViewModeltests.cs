using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FluentAssertions;
using Moq;
using Prism.Events;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Menus;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MenuBarViewModeltests
	{
		public MenuBarViewModeltests()
		{
		    _eventAggregatorMock = new Mock<IEventAggregator>();

		    _languageChangedEventMock = new Mock<LanguageChangedEvent>();
		    _eventAggregatorMock
		        .Setup(ea => ea.GetEvent<LanguageChangedEvent>())
		        .Returns(_languageChangedEventMock.Object);

            _menuBarViewModel = new MenuBarViewModel(_eventAggregatorMock.Object);
		}
        
		private readonly IMenuBarViewModel _menuBarViewModel;
	    private readonly Mock<LanguageChangedEvent> _languageChangedEventMock;
        private readonly Mock<IEventAggregator> _eventAggregatorMock;

	    [Fact]
	    public void Should_publish_language_changed_event()
	    {
            _menuBarViewModel.ChangeLanguageCommand.Execute(null);
            _languageChangedEventMock.Verify(e=>e.Publish(), Times.Once);
	    }
    }
}