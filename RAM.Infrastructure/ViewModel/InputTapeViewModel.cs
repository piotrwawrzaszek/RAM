using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Events.FileRecordEvents;
using RAM.Infrastructure.Events.MenuItemEvents;
using RAM.Infrastructure.Resources.Controls;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
    public interface IInputTapeViewModel : IViewModel
    {
        string NumberHeader { get; }
        string ValueHeader { get; }

        TapeMemberWrapper SelectedTapeMember { get; set; }
        ObservableCollection<TapeMemberWrapper> TapeMembers { get; set; }
    }

    public class InputTapeViewModel : ViewModelBase, IInputTapeViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IImputMembersProvider _imputMembersProvider;

        public InputTapeViewModel(IImputMembersProvider imputMembersProvider,
            IEventAggregator eventAggregator)
        {
            _imputMembersProvider = imputMembersProvider;
            _eventAggregator = eventAggregator;
            SubscribeToEvents();

            TapeMembers = new ObservableCollection<TapeMemberWrapper>(
                _imputMembersProvider.GetAllTapeMembers().Select(x => new TapeMemberWrapper(x)));
            LoadLocalizationStrings();
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

        protected sealed override void SubscribeToEvents()
        {
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            _eventAggregator.GetEvent<AddAboveEvent<TapeMemberWrapper>>().Subscribe(AddAboveEventHandler);
            _eventAggregator.GetEvent<AddBelowEvent<TapeMemberWrapper>>().Subscribe(AddBelowEventHandler);
            _eventAggregator.GetEvent<PasteEvent<TapeMemberWrapper>>().Subscribe(PasteEventHandler);
            _eventAggregator.GetEvent<DeleteEvent<TapeMemberWrapper>>().Subscribe(DeleteEventHandler);
            _eventAggregator.GetEvent<ClearEvent<TapeMemberWrapper>>().Subscribe(ClearEventHandler);
            _eventAggregator.GetEvent<LoadInputMembersEvent>().Subscribe(LoadInputMembersHandler);
        }

        protected sealed override void LoadLocalizationStrings()
        {
            NumberHeader = Controls.NumberHeader;
            ValueHeader = Controls.ValueHeader;
        }

        private void AddAboveEventHandler(TapeMemberWrapper tapeMember)
        {
            var index = TapeMembers.IndexOf(tapeMember);
            TapeMembers.Insert(index, new TapeMemberWrapper());
        }

        private void AddBelowEventHandler(TapeMemberWrapper tapeMember)
        {
            var index = TapeMembers.IndexOf(tapeMember);
            TapeMembers.Insert(index + 1, new TapeMemberWrapper());
        }

        private void PasteEventHandler(TapeMemberWrapper tapeMember)
        {
            if (tapeMember == null) return;
            var index = TapeMembers.IndexOf(tapeMember);
            TapeMembers.Insert(index, tapeMember);
        }

        private void DeleteEventHandler(TapeMemberWrapper tapeMember)
        {
            TapeMembers.Remove(tapeMember);
            if (TapeMembers.Count == 0)
                TapeMembers.Add(new TapeMemberWrapper());
        }

        private void ClearEventHandler()
        {
            TapeMembers.Clear();
            TapeMembers.Add(new TapeMemberWrapper());
        }

        private void LoadInputMembersHandler(FileRecordWrapper fileRecord)
        {
            TapeMembers =
                new ObservableCollection<TapeMemberWrapper>(
                    fileRecord.InputMembers.Select(x => new TapeMemberWrapper(x)));
        }

        #endregion
    }
}