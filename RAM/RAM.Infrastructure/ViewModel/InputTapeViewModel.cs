using System.Collections.ObjectModel;
using RAM.Domain.Model;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
	public interface IInputTapeViewModel : IViewModel
	{
		TapeMemberWrapper SelectedTapeMember { get; set; }
		ObservableCollection<TapeMemberWrapper> TapeMembers { get; set; }
	}

	public class InputTapeViewModel : BaseViewModel, IInputTapeViewModel
	{
		private TapeMemberWrapper _selectedTapeMember;
		private ObservableCollection<TapeMemberWrapper> _tapeMembers;

		public InputTapeViewModel()
		{
			_tapeMembers = new ObservableCollection<TapeMemberWrapper>
			{
				new TapeMemberWrapper(new TapeMember(1, "aaa")),
				new TapeMemberWrapper(new TapeMember(2, "4")),
				new TapeMemberWrapper(new TapeMember(3, "bbc")),
			};
		}

		public TapeMemberWrapper SelectedTapeMember
		{
			get => _selectedTapeMember;
			set => SetProperty(ref _selectedTapeMember, value);
		}

		public ObservableCollection<TapeMemberWrapper> TapeMembers
		{
			get => _tapeMembers;
			set => SetProperty(ref _tapeMembers, value);
		}
	}
}