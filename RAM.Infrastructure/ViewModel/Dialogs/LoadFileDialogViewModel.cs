using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Prism.Events;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Events.FileRecordEvents;
using RAM.Infrastructure.Resources.Controls;
using RAM.Infrastructure.Resources.Messages;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel.Dialogs
{
    public interface ILoadFileDialogViewModel : IDialogViewModel
    {
        string NameHeader { get; }
        string CommentHeader { get; }
        string CreatedAtHeader { get; }
        string LoadInputMembersText { get; }

        bool LoadInputIsChecked { get; set; }
        bool ShowDeleteWarning { get; }

        ICommand OkCommand { get; }
        ICommand CancelCommand { get; }
        ICommand DeleteCommand { get; }

        FileRecordWrapper SelectedRecord { get; set; }
        ObservableCollection<FileRecordWrapper> FileRecords { get; set; }
    }

    public class LoadFileDialogViewModel : DialogViewModelBase, ILoadFileDialogViewModel
    {
        private readonly IFileDataProvider _fileDataProvider;
        private readonly IEventAggregator _eventAggregator;
        
        public LoadFileDialogViewModel(IFileDataProvider fileDataProvider, IEventAggregator eventAggregator)
        {
            _fileDataProvider = fileDataProvider;
            _eventAggregator = eventAggregator;

            FileRecords = _fileDataProvider.GetAllRecords();
            SubscribeToEvents();

            SeedCommands();
            SelectedRecord.PropertyChanged += (sender, args) => InvalidateCommands();
            LoadLocalizationStrings();
        }

        #region Event handlers

        protected sealed override void SubscribeToEvents()
        {
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
        }

        protected sealed override void LoadLocalizationStrings()
        {
            base.LoadLocalizationStrings();
            CommentHeader = Controls.CommentHeader;
            CreatedAtHeader = Controls.CreatedAtHeader;
            NameHeader = Controls.NameHeader;
            DialogTitle = Controls.LoadDialogHeader;
            LoadInputMembersText = Controls.LoadInputMembersText;
        }

        private void CreateMessage(string message)
        {
            _eventAggregator.GetEvent<MessageCreatedEvent>().Publish(message);
        }

        #endregion

        #region Properties
        
        private string _commentHeader;
        private string _createdAtHeader;
        private string _nameHeader;
        private string _dialogTitle;
        private string _loadInputMembersText;

        private bool _loadInputIsChecked;
        private bool _showDeleteWarning;

        private FileRecordWrapper _selectedRecord;
        private ObservableCollection<FileRecordWrapper> _fileRecords;

        public string DialogTitle
        {
            get => _dialogTitle;
            protected set => SetProperty(ref _dialogTitle, value);
        }

        public string NameHeader
        {
            get => _nameHeader;
            protected set => SetProperty(ref _nameHeader, value);
        }

        public string CommentHeader
        {
            get => _commentHeader;
            protected set => SetProperty(ref _commentHeader, value);
        }

        public string CreatedAtHeader
        {
            get => _createdAtHeader;
            protected set => SetProperty(ref _createdAtHeader, value);
        }

        public string LoadInputMembersText
        {
            get => _loadInputMembersText;
            protected set => SetProperty(ref _loadInputMembersText, value);
        }

        public bool LoadInputIsChecked
        {
            get => _loadInputIsChecked;
            set => SetProperty(ref _loadInputIsChecked, value);
        }
        public bool ShowDeleteWarning
        {
            get => _showDeleteWarning;
            protected set => SetProperty(ref _showDeleteWarning, value);
        }

        public ObservableCollection<FileRecordWrapper> FileRecords
        {
            get => _fileRecords;
            set => SetProperty(ref _fileRecords, value);
        }

        public FileRecordWrapper SelectedRecord
        {
            get => _selectedRecord;
            set => SetProperty(ref _selectedRecord, value);
        }

        public ICommand OkCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }
        public ICommand SwitchLoadInputCommand { get; protected set; }

        #endregion

        #region Command methods

        protected sealed override void SeedCommands()
        {
            OkCommand = new RelayCommand(OkExecute, OkCanExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            DeleteCommand = new RelayCommand(DeleteExecute, DeleteCanExecute);
        }

        protected override void InvalidateCommands()
        {
            ((RelayCommand) OkCommand).RaiseCanExecuteChanged();
            ((RelayCommand) DeleteCommand).RaiseCanExecuteChanged();
        }

        private void OkExecute(object sender)
        {
            if(!(sender is Window window)) return;
            _eventAggregator.GetEvent<LoadStatementsEvent>().Publish(SelectedRecord);
            if(LoadInputIsChecked)
                _eventAggregator.GetEvent<LoadInputMembersEvent>().Publish(SelectedRecord);
            window.Close();
            CreateMessage(Messages.FileRecordLoaded);
        }

        private bool OkCanExecute(object sender)
            => SelectedRecord != null && FileRecords.Contains(SelectedRecord);

        private static void CancelExecute(object sender)
        {
            if (sender is Window window) window.Close();
        }

        private void DeleteExecute(object sender)
        {
            if (!ShowDeleteWarning) DeleteRecord();
            else
            {
            //    var result = _messageDialogService.ShowYesNoDialog(Controls.DeleteRecordTitle,
            //        $"{Controls.DeleteRecordMessage} {SelectedRecord.Name}");
            //    if(result) DeleteRecord();
            }
            CreateMessage(Messages.FileRecordDeleted);
        }

        private void DeleteRecord()
        {
            _fileDataProvider.DeleteRecord(SelectedRecord.Id);
            var index = _fileRecords.IndexOf(SelectedRecord);
            _fileRecords.Remove(SelectedRecord);

            if (index - 1 < 0) index = 0;
            if (_fileRecords.Count <= 0)
            {
                SelectedRecord = null;
                return;
            }
            SelectedRecord = _fileRecords[index - 1];
        }

        private bool DeleteCanExecute(object sender)
            => SelectedRecord != null && FileRecords.Contains(SelectedRecord);

        #endregion
    }
}