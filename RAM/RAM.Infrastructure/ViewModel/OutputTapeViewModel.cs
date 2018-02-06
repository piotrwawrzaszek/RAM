using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Domain.Model;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
	public interface IOutputTapeViewModel : IViewModel
	{
		TapeMemberWrapper SelectedTapeMember { get; set; }
		ObservableCollection<TapeMemberWrapper> TapeMembers { get; set; }
	}

	public class OutputTapeViewModel : BaseViewModel, IOutputTapeViewModel
	{
		private TapeMemberWrapper _selectedTapeMember;
		private ObservableCollection<TapeMemberWrapper> _tapeMembers;

		public OutputTapeViewModel()
		{
			_tapeMembers = new ObservableCollection<TapeMemberWrapper>
			{
				new TapeMemberWrapper(1, "aaa"),
				new TapeMemberWrapper(2, "4"),
				new TapeMemberWrapper(3, "bbc"),
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
