using FluentAssertions;
using RAM.Infrastructure.ViewModel;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MainWindowViewModelTests
	{
		public MainWindowViewModelTests()
		{
			_mainWindowViewModel = new MainWindowViewModel();
		}

		private readonly IMainWindowViewModel _mainWindowViewModel;

		[Fact]
		public void Should_raise_property_changed_event_for_header()
		{
			_mainWindowViewModel.MonitorEvents();
			_mainWindowViewModel.Header = "Different header";
			_mainWindowViewModel.ShouldRaisePropertyChangeFor(x => x.Header);
		}
	}
}