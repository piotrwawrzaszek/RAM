using FluentAssertions;
using RAM.Infrastructure.ViewModel;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MainWindowViewModelTests
	{
		private readonly IMainWindowViewModel _mainWindowViewModel;

		public MainWindowViewModelTests()
		{
			_mainWindowViewModel = new MainWindowViewModel();
		}

		[Fact]
		public void Should_raise_property_changed_event_for_header()
		{
			_mainWindowViewModel.MonitorEvents();
			_mainWindowViewModel.Header = "Different header";
			_mainWindowViewModel.ShouldRaisePropertyChangeFor(x => x.Header);
		}
	}
}