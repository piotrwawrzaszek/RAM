using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Events;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Resources.Controls;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel.Dialogs
{
    public interface ILoadFileDialogViewModel : IViewModel
    {
        string NameHeader { get; }
        string CommentHeader { get; }
        string DialogTitle { get; }
        string OkButtonText { get; }
        string CancelButtonText { get; }
        string DeleteButtonText { get; }
        string LoadInputMembersText { get; }

        ICommand OkCommand { get; }
        ICommand CancelCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand SwitchLoadInputCommand { get; }

        FileRecordWrapper SelectedRecord { get; set; }
        ObservableCollection<FileRecordWrapper> SavedFileRecords { get; set; }
    }

    public class LoadFileDialogViewModel : BaseViewModel, ILoadFileDialogViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private FileRecordWrapper _selectedRecord;
        private ObservableCollection<FileRecordWrapper> _savedFileRecords;

        public LoadFileDialogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);

            SeedCommands();
            SelectedRecord.PropertyChanged += (sender, args) => InvalidateCommands();

            LoadLocalizationStrings();
        }

        #region Event handlers

        private void LoadLocalizationStrings()
        {
            DialogTitle = Controls.LoadDialogHeader;
            OkButtonText = Controls.OkButtonText;
            CancelButtonText = Controls.CancelButtonText;
            DeleteButtonText = Controls.DeleteButtonText;
            LoadInputMembersText = Controls.LoadInputMembersText;
        }

        #endregion

        #region Properties

        private string _cancelButtonText;
        private string _deleteButtonText;
        private string _commentHeader;
        private string _nameHeader;
        private string _dialogTitle;
        private string _loadInputMembersText;
        private string _okButtonText;

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

        public string OkButtonText
        {
            get => _okButtonText;
            protected set => SetProperty(ref _okButtonText, value);
        }

        public string CancelButtonText
        {
            get => _cancelButtonText;
            protected set => SetProperty(ref _cancelButtonText, value);
        }

        public string DeleteButtonText
        {
            get => _deleteButtonText;
            protected set => SetProperty(ref _deleteButtonText, value);
        }

        public string LoadInputMembersText
        {
            get => _loadInputMembersText;
            protected set => SetProperty(ref _loadInputMembersText, value);
        }

        public ObservableCollection<FileRecordWrapper> SavedFileRecords
        {
            get => _savedFileRecords;
            set => SetProperty(ref _savedFileRecords, value);
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

        private void SeedCommands()
        {
            OkCommand = new RelayCommand(OkExecute, OkCanExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            DeleteCommand = new RelayCommand(DeleteExecute, DeleteCanExecute);
            SwitchLoadInputCommand = new RelayCommand(SwitchLoadInputExecute);
        }

        private void InvalidateCommands()
        {
            ((RelayCommand) OkCommand).RaiseCanExecuteChanged();
            ((RelayCommand) DeleteCommand).RaiseCanExecuteChanged();
        }

        private void OkExecute(object sender)
        {
            if (!(sender is Button) && !(sender is FileRecordWrapper)) return;

            //var index = _statements.IndexOf(statement);
            //_statements.Insert(index, StatementWrapper.GetEmptyInstance());
            
        }

        private bool OkCanExecute(object sender)
            => SelectedRecord != null && SavedFileRecords.Contains(SelectedRecord);

        private void CancelExecute(object sender)
        {
            if (!(sender is Button)) return;

            //if (!(sender is StatementWrapper statement)) return;

            //var index = _statements.IndexOf(statement);
            //_statements.Insert(index + 1, StatementWrapper.GetEmptyInstance());

        }

        private void DeleteExecute(object sender)
        {
            //if (!(sender is StatementWrapper statement)) return;
            //_statements.Remove(statement);

            //if (_statements.Count == 0)
            //    _statements.Add(StatementWrapper.GetEmptyInstance());
         
        }

        private bool DeleteCanExecute(object sender)
            => SelectedRecord != null && SavedFileRecords.Contains(SelectedRecord);

        private void SwitchLoadInputExecute(object sender)
        {
            //    if (!(sender is StatementWrapper)) return;
            //    _statements.Clear();
            //    _statements.Add(StatementWrapper.GetEmptyInstance());
           
        }

        #endregion
    }
}