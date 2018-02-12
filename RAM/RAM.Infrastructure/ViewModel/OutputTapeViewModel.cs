using System.Collections.ObjectModel;
using Prism.Events;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Resources.Controls;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
	public interface IOutputTapeViewModel : IViewModel
	{
	    string Number { get; set; }
	    string Value { get; set; }

        TapeMemberWrapper SelectedTapeMember { get; set; }
		ObservableCollection<TapeMemberWrapper> TapeMembers { get; set; }
	}

	public class OutputTapeViewModel : BaseViewModel, IOutputTapeViewModel
	{
	    private readonly IEventAggregator _eventAggregator;

	    private string _number;
	    private string _value;

        private TapeMemberWrapper _selectedTapeMember;
		private ObservableCollection<TapeMemberWrapper> _tapeMembers;
        
        public OutputTapeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            LoadLocalizationStrings();

            _tapeMembers = new ObservableCollection<TapeMemberWrapper>
			{
				new TapeMemberWrapper(1, "aaa"),
				new TapeMemberWrapper(2, "4"),
				new TapeMemberWrapper(3, "bbc"),
			};
		}

	    #region Properties

	    public string Number
	    {
	        get => _number;
	        set => SetProperty(ref _number, value);
	    }

	    public string Value
	    {
	        get => _value;
	        set => SetProperty(ref _value, value);
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

        #endregion

	    #region Event handlers

	    private void LoadLocalizationStrings()
	    {
	        Number = Controls.Number;
	        Value = Controls.Value;
	    }

	    #endregion
    }
}
