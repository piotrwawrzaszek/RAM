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
    public interface IStatementGridViewModel : IViewModel
    {
        string LabelHeader { get; }
        string InstructionHeader { get; }
        string ArgumentHeader { get; }
        string CommentHeader { get; }

        StatementWrapper SelectedStatement { get; set; }
        ObservableCollection<StatementWrapper> Statements { get; set; }
    }

    public class StatementGridViewModel : ViewModelBase, IStatementGridViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStatementProvider _statementProvider;
        
        public StatementGridViewModel(IStatementProvider statementProvider,
            IEventAggregator eventAggregator)
        {
            _statementProvider = statementProvider;
            _eventAggregator = eventAggregator;
            SubscribeToEvents();

            Statements = new ObservableCollection<StatementWrapper>(
                _statementProvider.GetAllStatements().Select(x => new StatementWrapper(x)));
            LoadLocalizationStrings();
        }

        #region Properties

        private string _argumentHeader;
        private string _commentHeader;
        private string _instructionHeader;
        private string _labelHeader;

        private StatementWrapper _selectedStatement;
        private ObservableCollection<StatementWrapper> _statements;

        public string LabelHeader
        {
            get => _labelHeader;
            protected set => SetProperty(ref _labelHeader, value);
        }

        public string InstructionHeader
        {
            get => _instructionHeader;
            protected set => SetProperty(ref _instructionHeader, value);
        }

        public string ArgumentHeader
        {
            get => _argumentHeader;
            protected set => SetProperty(ref _argumentHeader, value);
        }

        public string CommentHeader
        {
            get => _commentHeader;
            protected set => SetProperty(ref _commentHeader, value);
        }

        public StatementWrapper SelectedStatement
        {
            get => _selectedStatement;
            set => SetProperty(ref _selectedStatement, value);
        }

        public ObservableCollection<StatementWrapper> Statements
        {
            get => _statements;
            set => SetProperty(ref _statements, value);
        }

        #endregion

        #region Event handlers

        protected  sealed override void SubscribeToEvents()
        {
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            _eventAggregator.GetEvent<AddAboveEvent<StatementWrapper>>().Subscribe(AddAboveEventHandler);
            _eventAggregator.GetEvent<AddBelowEvent<StatementWrapper>>().Subscribe(AddBelowEventHandler);
            _eventAggregator.GetEvent<PasteEvent<StatementWrapper>>().Subscribe(PasteEventHandler);
            _eventAggregator.GetEvent<DeleteEvent<StatementWrapper>>().Subscribe(DeleteEventHandler);
            _eventAggregator.GetEvent<ClearEvent<StatementWrapper>>().Subscribe(ClearEventHandler);
            _eventAggregator.GetEvent<LoadStatementsEvent>().Subscribe(LoadStatementsHandler);
        }

        protected sealed override void LoadLocalizationStrings()
        {
            LabelHeader = Controls.LabelHeader;
            InstructionHeader = Controls.InstructionHeader;
            ArgumentHeader = Controls.ArgumentHeader;
            CommentHeader = Controls.CommentHeader;
        }

        private void AddAboveEventHandler(StatementWrapper statement)
        {
            var index = Statements.IndexOf(statement);
            Statements.Insert(index, new StatementWrapper());
        }

        private void AddBelowEventHandler(StatementWrapper statement)
        {
            var index = Statements.IndexOf(statement);
            Statements.Insert(index + 1, new StatementWrapper());
        }

        private void PasteEventHandler(StatementWrapper statement)
        {
            if (statement == null) return;
            var index = Statements.IndexOf(statement);
            Statements.Insert(index, statement);
        }

        private void DeleteEventHandler(StatementWrapper statement)
        {
            Statements.Remove(statement);
            if (Statements.Count == 0)
                Statements.Add(new StatementWrapper());
        }

        private void ClearEventHandler()
        {
            Statements.Clear();
            Statements.Add(new StatementWrapper());
        }

        private void LoadStatementsHandler(FileRecordWrapper fileRecord)
        {
            Statements =
                new ObservableCollection<StatementWrapper>(
                    fileRecord.Statements.Select(x => new StatementWrapper(x)));
        }

        #endregion
    }
}