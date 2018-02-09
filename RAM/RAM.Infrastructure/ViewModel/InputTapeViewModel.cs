using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using RAM.Domain.Model;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Resources.Controls;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;
using RAM.Infrastructure.Resources.MenuItems;
using static RAM.Infrastructure.ViewModel.MenuItemViewModel;

namespace RAM.Infrastructure.ViewModel
{
    public interface IInputTapeViewModel : IViewModel
    {
        string Number { get; set; }
        string Value { get; set; }
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
            eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);

            MenuItemViewModels = Seed();
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

        private ObservableCollection<IMenuItemViewModel> Seed()
        {
            var menuItems = new ObservableCollection<IMenuItemViewModel>
            {
                LoadInstance(() => MenuItems.Paste, new RelayCommand(PasteExecute)),
                LoadInstance(() => MenuItems.Copy, new RelayCommand(CopyExecute)),
                LoadInstance(() => MenuItems.Cut, new RelayCommand(CutExecute)),
                LoadInstance(() => MenuItems.AddAbove, new RelayCommand(AddAboveExecute)),
                LoadInstance(() => MenuItems.AddBelow, new RelayCommand(AddBelowExecute)),
                LoadInstance(() => MenuItems.Delete, new RelayCommand(DeleteExecute)),
                LoadInstance(() => MenuItems.ClearTape, new RelayCommand(ClearTapeExecute))
            };
            return menuItems;
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