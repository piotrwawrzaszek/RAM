using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Resources.Controls;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
    public interface IInputTapeViewModel : IViewModel
    {
        string Number { get; }
        string Value { get; }

        TapeMemberWrapper SelectedTapeMember { get; set; }
        ObservableCollection<TapeMemberWrapper> TapeMembers { get; set; }
    }

    public class InputTapeViewModel : BaseViewModel, IInputTapeViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IImputMembersProvider _imputMembersProvider;

        private TapeMemberWrapper _selectedTapeMember;
        private ObservableCollection<TapeMemberWrapper> _tapeMembers;
       
        public InputTapeViewModel(IImputMembersProvider imputMembersProvider,
            IEventAggregator eventAggregator) 
        {
            _imputMembersProvider = imputMembersProvider;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            
            _tapeMembers = new ObservableCollection<TapeMemberWrapper>(
                _imputMembersProvider.GetAllTapeMembers().Select(x => new TapeMemberWrapper(x)));
            LoadLocalizationStrings();
        }

        #region Event handlers

        protected sealed override void LoadLocalizationStrings()
        {
            Number = Controls.Number;
            Value = Controls.Value;
        }

        #endregion

        #region Properties

        private string _number;
        private string _value;

        public string Number
        {
            get => _number;
            protected set => SetProperty(ref _number, value);
        }

        public string Value
        {
            get => _value;
            protected set => SetProperty(ref _value, value);
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
    }
}