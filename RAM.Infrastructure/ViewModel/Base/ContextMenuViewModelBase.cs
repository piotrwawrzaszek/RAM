using System.Collections.Generic;
using System.Windows.Input;
using Prism.Events;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Events.MenuItemEvents;
using RAM.Infrastructure.Resources.MenuItems;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel.Base
{
    public interface IContextMenuViewModel : IViewModel
    {
        bool IsAddVisible { get; set; }
        bool IsAddWithOptionsVisible { get; set; }

        string Copy { get; }
        string Cut { get; }
        string Paste { get; }
        string Add { get; }
        string Above { get; }
        string Below { get; }
        string Delete { get; }
        string Clear { get; }

        ICommand PasteCommand { get; }
        ICommand CopyCommand { get; }
        ICommand CutCommand { get; }
        ICommand AddAboveCommand { get; }
        ICommand AddBelowCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand ClearCommand { get; }
    }

    public abstract class ContextMenuViewModelBase<T> : ViewModelBase, IContextMenuViewModel where T : IWrapper, new()
    {
        private readonly IEventAggregator _eventAggregator;
        private string _above;
        private string _add;
        private string _below;
        private string _clear;
        private string _copy;
        private string _cut;
        private string _delete;
        private string _paste;

        private bool _isAddVisible;
        private bool _isAddWithOptionsVisible;

        protected ContextMenuViewModelBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            IsAddWithOptionsVisible = true;
            LoadLocalizationStrings();
            SeedCommands();
        }

        #region Event handlers

        protected sealed override void LoadLocalizationStrings()
        {
            Copy = MenuItems.Copy;
            Cut = MenuItems.Cut;
            Paste = MenuItems.Paste;
            Add = MenuItems.Add;
            Above = MenuItems.Above;
            Below = MenuItems.Below;
            Delete = MenuItems.Delete;
            Clear = MenuItems.Clear;
        }

        #endregion

        #region Properties

        public bool IsAddVisible 
        {
            get => _isAddVisible;
            set
            {
                if (_isAddVisible == value) return;
                _isAddVisible = value;
                IsAddWithOptionsVisible = !value;
                OnPropertyChanged();
            }
        }

        public bool IsAddWithOptionsVisible
        {
            get => _isAddWithOptionsVisible;
            set
            {
                if (_isAddWithOptionsVisible == value) return;
                _isAddWithOptionsVisible = value;
                IsAddVisible = !value;
                OnPropertyChanged();
            }
        }

        public string Copy
        {
            get => _copy;
            protected set => SetProperty(ref _copy, value);
        }

        public string Cut
        {
            get => _cut;
            protected set => SetProperty(ref _cut, value);
        }

        public string Add
        {
            get => _add;
            protected set => SetProperty(ref _add, value);
        }

        public string Above
        {
            get => _above;
            protected set => SetProperty(ref _above, value);
        }

        public string Below
        {
            get => _below;
            protected set => SetProperty(ref _below, value);
        }

        public string Delete
        {
            get => _delete;
            protected set => SetProperty(ref _delete, value);
        }

        public string Clear
        {
            get => _clear;
            protected set => SetProperty(ref _clear, value);
        }

        public string Paste
        {
            get => _paste;
            protected set => SetProperty(ref _paste, value);
        }

        public ICommand PasteCommand { get; protected set; }
        public ICommand CopyCommand { get; protected set; }
        public ICommand CutCommand { get; protected set; }
        public ICommand AddAboveCommand { get; protected set; }
        public ICommand AddBelowCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }
        public ICommand ClearCommand { get; protected set; }

        #endregion

        #region Command methods

        private T _clipboard = new T();

        protected T Clipboard
        {
            get => _clipboard;
            set 
            {
                if(EqualityComparer<T>.Default.Equals(_clipboard, value)) return;
                _clipboard = value;
                InvalidateCommands();
                OnPropertyChanged();
            }
        }

        private bool IsClipboardNullOrEqual =>
            Clipboard != null && !EqualityComparer<T>.Default.Equals(Clipboard, new T());

        private void AddAboveExecute(object sender)
        {
            if (sender is T item)
                _eventAggregator.GetEvent<AddAboveEvent<T>>().Publish(item);
        }

        private void AddBelowExecute(object sender)
        {
            if (sender is T item)
                _eventAggregator.GetEvent<AddBelowEvent<T>>().Publish(item);
        }

        private void DeleteExecute(object sender)
        {
            if (sender is T item)
                _eventAggregator.GetEvent<DeleteEvent<T>>().Publish(item);
        }

        private void ClearExecute(object sender)
        {
            if (sender is T)
                _eventAggregator.GetEvent<ClearEvent<T>>().Publish();
        }

        private void PasteExecute(object sender)
        {
            if (sender is T item)
                _eventAggregator.GetEvent<PasteEvent<T>>().Publish(item);
        }

        private bool PasteCanExecute(object sender)
            => IsClipboardNullOrEqual;


        private void CopyExecute(object sender)
        {
            if (sender is T item) 
                Clipboard = item;
        }

        private bool CopyCanExecute(object sender)
            => IsClipboardNullOrEqual;

        private void CutExecute(object sender)
        {
            if (!(sender is T item)) return;
            Clipboard = item;
            _eventAggregator.GetEvent<DeleteEvent<T>>().Publish(item);
        }

        private bool CutCanExecute(object sender)
            => IsClipboardNullOrEqual;
        
        protected sealed override void SeedCommands()
        {
            PasteCommand = new RelayCommand(PasteExecute, PasteCanExecute);
            CopyCommand = new RelayCommand(CopyExecute, CopyCanExecute);
            CutCommand = new RelayCommand(CutExecute, CutCanExecute);
            AddAboveCommand = new RelayCommand(AddAboveExecute);
            AddBelowCommand = new RelayCommand(AddBelowExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            ClearCommand = new RelayCommand(ClearExecute);
        }

        protected override void InvalidateCommands()
        {
            ((RelayCommand) PasteCommand).RaiseCanExecuteChanged();
            ((RelayCommand) CopyCommand).RaiseCanExecuteChanged();
            ((RelayCommand) CutCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}