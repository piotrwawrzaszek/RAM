using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Events;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Resources.Controls;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;
using RAM.Infrastructure.Resources.MenuItems;

namespace RAM.Infrastructure.ViewModel
{
    public interface IInputTapeViewModel : IViewModel
    {
        string Number { get; set; }
        string Value { get; set; }

        ICommand PasteCommand { get; }
        ICommand CopyCommand { get; }
        ICommand CutCommand { get; }
        ICommand AddAboveCommand { get; }
        ICommand AddBelowCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand ClearTapeCommand { get; }

        TapeMemberWrapper SelectedTapeMember { get; set; }
        ObservableCollection<TapeMemberWrapper> TapeMembers { get; set; }
        ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; }
    }

    public class InputTapeViewModel : BaseViewModel, IInputTapeViewModel
    {
        private readonly ITapeMemberProvider _tapeMemberProvider;
        private readonly IEventAggregator _eventAggregator;

        private string _number;
        private string _value;
        private TapeMemberWrapper _selectedTapeMember;
        private ObservableCollection<TapeMemberWrapper> _tapeMembers;

        public InputTapeViewModel(ITapeMemberProvider tapeMemberProvider,
            IEventAggregator eventAggregator)
        {
            _tapeMemberProvider = tapeMemberProvider;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);

            SeedCommands();
            SeedMenuItems();

            _tapeMembers = new ObservableCollection<TapeMemberWrapper>(
                _tapeMemberProvider.GetAllTapeMembers().Select(x => new TapeMemberWrapper(x)));
            LoadLocalizationStrings();
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

        public ICommand PasteCommand { get; protected set; }
        public ICommand CopyCommand { get; protected set; }
        public ICommand CutCommand { get; protected set; }
        public ICommand AddAboveCommand { get; protected set; }
        public ICommand AddBelowCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }
        public ICommand ClearTapeCommand { get; protected set; }

        public ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; protected set; }

        #endregion

        #region Event handlers
        
        private void LoadLocalizationStrings()
        {
            Number = Controls.Number;
            Value = Controls.Value;
        }

        #endregion

        #region Menu items creation

        private void SeedCommands()
        {
            PasteCommand = new RelayCommand(PasteExecute);
            CopyCommand = new RelayCommand(CopyExecute);
            CutCommand = new RelayCommand(CutExecute);
            AddAboveCommand = new RelayCommand(AddAboveExecute);
            AddBelowCommand = new RelayCommand(AddBelowExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            ClearTapeCommand = new RelayCommand(ClearTapeExecute);
        }

        private void SeedMenuItems()
        {
            MenuItemViewModels = new ObservableCollection<IMenuItemViewModel>
            {
                new MenuItemViewModel(() => MenuItems.Paste, _eventAggregator).Load(PasteCommand),
                new MenuItemViewModel(() => MenuItems.Copy, _eventAggregator).Load(CopyCommand),
                new MenuItemViewModel(() => MenuItems.Cut, _eventAggregator).Load(CutCommand),
                new MenuItemViewModel(() => MenuItems.AddAbove, _eventAggregator).Load(AddAboveCommand),
                new MenuItemViewModel(() => MenuItems.AddBelow, _eventAggregator).Load(AddBelowCommand),
                new MenuItemViewModel(() => MenuItems.Delete, _eventAggregator).Load(DeleteCommand),
                new MenuItemViewModel(() => MenuItems.ClearTape, _eventAggregator).Load(ClearTapeCommand)
            };
        }

        #endregion

        #region Command methods

        private void AddAboveExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;

            var index = _tapeMembers.IndexOf(tapeMember);
            _tapeMembers.Insert(index, TapeMemberWrapper.GetEmptyInstance());
        }

        private void AddBelowExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;

            var index = _tapeMembers.IndexOf(tapeMember);
            _tapeMembers.Insert(index + 1, TapeMemberWrapper.GetEmptyInstance());
        }

        private void DeleteExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;
            _tapeMembers.Remove(tapeMember);

            if (_tapeMembers.Count == 0)
                _tapeMembers.Add(TapeMemberWrapper.GetEmptyInstance());
        }

        private void ClearTapeExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper)) return;
            _tapeMembers.Clear();
            _tapeMembers.Add(TapeMemberWrapper.GetEmptyInstance());
        }

        private static TapeMemberWrapper _clipboard;

        private void PasteExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;
            if (_clipboard == null) return;

            var index = _tapeMembers.IndexOf(tapeMember);
            _tapeMembers.Insert(index, _clipboard);
        }

        private static void CopyExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;
            _clipboard = new TapeMemberWrapper(tapeMember);
        }

        private void CutExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;

            _tapeMembers.Remove(tapeMember);
            _clipboard = new TapeMemberWrapper(tapeMember);
        }

        #endregion
    }
}