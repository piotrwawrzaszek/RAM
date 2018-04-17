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
	    string NumberHeader { get; }
	    string ValueHeader { get; }

        TapeMemberWrapper SelectedTapeMember { get; set; }
		ObservableCollection<TapeMemberWrapper> TapeMembers { get; set; }
	}

	public class OutputTapeViewModel : ViewModelBase, IOutputTapeViewModel
	{
	    private readonly IEventAggregator _eventAggregator;
        
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
        
	    private string _numberHeader;
	    private string _valueHeader;

	    private TapeMemberWrapper _selectedTapeMember;
	    private ObservableCollection<TapeMemberWrapper> _tapeMembers;

        public string NumberHeader
        {
	        get => _numberHeader;
	        protected set => SetProperty(ref _numberHeader, value);
	    }

	    public string ValueHeader
        {
	        get => _valueHeader;
	        protected set => SetProperty(ref _valueHeader, value);
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

	    protected sealed override void LoadLocalizationStrings()
	    {
            NumberHeader = Controls.NumberHeader;
            ValueHeader = Controls.ValueHeader;
	    }

	    #endregion
    }
}
