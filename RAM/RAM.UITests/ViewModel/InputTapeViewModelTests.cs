using System.Collections.ObjectModel;
using FluentAssertions;
using Moq;
using RAM.Domain.Model;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Wrapper;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class InputTapeViewModelTests
	{
		public InputTapeViewModelTests()
		{
            _tapeMemberProviderMock = new Mock<ITapeMemberProvider>();
			_inputTapeViewModel = new InputTapeViewModel(_tapeMemberProviderMock.Object);
		}

		private readonly IInputTapeViewModel _inputTapeViewModel;
	    private readonly Mock<ITapeMemberProvider> _tapeMemberProviderMock;

		[Fact]
		public void Should_raise_property_changed_event_for_selected_tape_member()
		{
			_inputTapeViewModel.MonitorEvents();
			_inputTapeViewModel.SelectedTapeMember = new TapeMemberWrapper(new TapeMember(1, "A"));
			_inputTapeViewModel.ShouldRaisePropertyChangeFor(x => x.SelectedTapeMember);
		}

		[Fact]
		public void Should_raise_property_changed_event_for_tape_members()
		{
			_inputTapeViewModel.MonitorEvents();
			_inputTapeViewModel.TapeMembers = new ObservableCollection<TapeMemberWrapper>();
			_inputTapeViewModel.ShouldRaisePropertyChangeFor(x => x.TapeMembers);
		}
	}
}