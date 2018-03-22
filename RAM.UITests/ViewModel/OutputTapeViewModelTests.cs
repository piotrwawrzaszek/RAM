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
	public class OutputTapeViewModelTests
	{
		public OutputTapeViewModelTests()
		{
		    _eventAggregatorMock = new Mock<IEventAggregator>();

		    var languageChangedEventMock = new Mock<LanguageChangedEvent>();
		    _eventAggregatorMock
		        .Setup(ea => ea.GetEvent<LanguageChangedEvent>())
		        .Returns(languageChangedEventMock.Object);

            _outputTapeViewModel = new OutputTapeViewModel(_eventAggregatorMock.Object);
		}

	    private readonly Mock<IEventAggregator> _eventAggregatorMock;
        private readonly IOutputTapeViewModel _outputTapeViewModel;

		[Fact]
		public void Should_raise_property_changed_event_for_selected_tape_member()
		{
			_outputTapeViewModel.MonitorEvents();
			_outputTapeViewModel.SelectedTapeMember = new TapeMemberWrapper(new TapeMember(1, "A"));
			_outputTapeViewModel.ShouldRaisePropertyChangeFor(x => x.SelectedTapeMember);
		}

		[Fact]
		public void Should_raise_property_changed_event_for_tape_members()
		{
			_outputTapeViewModel.MonitorEvents();
			_outputTapeViewModel.TapeMembers = new ObservableCollection<TapeMemberWrapper>();
			_outputTapeViewModel.ShouldRaisePropertyChangeFor(x => x.TapeMembers);
		}
	}
}