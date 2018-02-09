using System.Globalization;
using FluentAssertions;
using Moq;
using Prism.Events;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.ViewModel;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MainWindowViewModelTests
	{
		public MainWindowViewModelTests()
		{
		    _eventAggregatorMock = new Mock<IEventAggregator>();

		    var languageChangedEventMock = new Mock<LanguageChangedEvent>();
		    _eventAggregatorMock
		        .Setup(ea => ea.GetEvent<LanguageChangedEvent>())
		        .Returns(languageChangedEventMock.Object);

		    _cultureInfoProviderMock = new Mock<ICultureInfoProvider>();

		    _cultureInfoProviderMock
		        .Setup(ci => ci.GetCultureInfo())
		        .Returns(new CultureInfo("en-GB"));

            _mainWindowViewModel = new MainWindowViewModel(_eventAggregatorMock.Object, 
                _cultureInfoProviderMock.Object);
		}

		private readonly IMainWindowViewModel _mainWindowViewModel;
	    private readonly Mock<IEventAggregator> _eventAggregatorMock;
	    private readonly Mock<ICultureInfoProvider> _cultureInfoProviderMock;

        [Fact]
		public void Should_raise_property_changed_event_for_header()
		{
			_mainWindowViewModel.MonitorEvents();
			_mainWindowViewModel.Header = "Different header";
			_mainWindowViewModel.ShouldRaisePropertyChangeFor(x => x.Header);
		}
	}
}