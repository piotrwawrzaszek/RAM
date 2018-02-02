using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
    public interface IInputTapeViewModel : IViewModel
    {
        TapeMemberWrapper SelectedTapeMember { get; set; }
        ObservableCollection<TapeMemberWrapper> TapeMembers { get; set; }
        ObservableCollection<IMenuItemViewModel> MenuItems { get; }
    }

    public class InputTapeViewModel : BaseViewModel, IInputTapeViewModel
    {
        private readonly ITapeMemberProvider _tapeMemberProvider;

        private TapeMemberWrapper _selectedTapeMember;
        private ObservableCollection<TapeMemberWrapper> _tapeMembers;

        public InputTapeViewModel(ITapeMemberProvider tapeMemberProvider)
        {
            _tapeMemberProvider = tapeMemberProvider;

            MenuItems = Seed();
            _tapeMembers = new ObservableCollection<TapeMemberWrapper>(
                _tapeMemberProvider.GetAllTapeMembers().Select(x => new TapeMemberWrapper(x)));
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

        public ObservableCollection<IMenuItemViewModel> MenuItems { get; protected set; }

        #region Menu items creation

        private ObservableCollection<IMenuItemViewModel> Seed()
        {
            var menuItems = new ObservableCollection<IMenuItemViewModel>
            {
                //MenuItemViewModel.LoadInstance(Resources.MenuItems.AddAboveEN, new RelayCommand(PasteExecute, PasteCanExecute)),
                //MenuItemViewModel.LoadInstance(Resources.MenuItems.AddAboveEN, new RelayCommand(CopyExecute, CopyCanExecute)),
                //MenuItemViewModel.LoadInstance(Resources.MenuItems.AddAboveEN, new RelayCommand(CutExecute, CutCanExecute)),
                MenuItemViewModel.LoadInstance(Resources.MenuItems.AddAboveEN, new RelayCommand(AddAboveExecute)),
                MenuItemViewModel.LoadInstance(Resources.MenuItems.AddBelowEN, new RelayCommand(AddBelowExecute)),
                MenuItemViewModel.LoadInstance(Resources.MenuItems.DeleteEN, new RelayCommand(DeleteExecute)),
                MenuItemViewModel.LoadInstance(Resources.MenuItems.ClearTapeEN, new RelayCommand(ClearTapeExecute))
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
        /*
        private const string Key = "tapeMember";

        private void PasteExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;

            var index = _tapeMembers.IndexOf(tapeMember);
            _tapeMembers.Insert(index, Clipboard.GetData(Key) as TapeMemberWrapper);
        }

        private static bool PasteCanExecute(object sender)
            => /*Clipboard.GetData(Key) is TapeMemberWrapper || *//*true;
        
        private static void CopyExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;
            Clipboard.Clear();
            Clipboard.SetData(Key, tapeMember);
        }

        private static bool CopyCanExecute(object sender)
            => true;


        private void CutExecute(object sender)
        {
            if (!(sender is TapeMemberWrapper tapeMember)) return;
            Clipboard.Clear();
            Clipboard.SetData(Key, tapeMember);
            _tapeMembers.Remove(tapeMember);
        }

        private static bool CutCanExecute(object sender)
            => true;
        */
        #endregion
    }
}