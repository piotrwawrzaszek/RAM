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
using static RAM.Infrastructure.ViewModel.MenuItemViewModel;

namespace RAM.Infrastructure.ViewModel
{
	public interface IStatementGridViewModel : IViewModel
	{
	    string Label { get; set; }
	    string Instruction { get; set; }
	    string Argument { get; set; }
	    string Comment { get; set; }

	    ICommand PasteCommand { get; }
	    ICommand CopyCommand { get; }
	    ICommand CutCommand { get; }
	    ICommand AddAboveCommand { get; }
	    ICommand AddBelowCommand { get; }
	    ICommand DeleteCommand { get; }
	    ICommand ClearTapeCommand { get; }

        StatementWrapper SelectedStatement { get; set; }
		ObservableCollection<StatementWrapper> Statements { get; set; }
	    ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; }
    }

    public class StatementGridViewModel : BaseViewModel, IStatementGridViewModel
	{
		private readonly IStatementProvider _statementProvider;
	    private readonly IEventAggregator _eventAggregator;

	    private string _label;
	    private string _instruction;
	    private string _argument;
	    private string _comment;
        private StatementWrapper _selectedStatement;
		private ObservableCollection<StatementWrapper> _statements;
        
	    public StatementGridViewModel(IStatementProvider statementProvider, IEventAggregator eventAggregator)
		{
			_statementProvider = statementProvider;
		    _eventAggregator = eventAggregator;
		    _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);

		    SeedCommands();
            SeedMenuItems();

			_statements = new ObservableCollection<StatementWrapper>(
				_statementProvider.GetAllStatements().Select(x => new StatementWrapper(x)));
		    LoadLocalizationStrings();
		}

        #region Properties

	    public string Label
	    {
	        get => _label;
	        set => SetProperty(ref _label, value);
	    }

	    public string Instruction
	    {
	        get => _instruction;
	        set => SetProperty(ref _instruction, value);
	    }

	    public string Argument
	    {
	        get => _argument;
	        set => SetProperty(ref _argument, value);
	    }

	    public string Comment
	    {
	        get => _comment;
	        set => SetProperty(ref _comment, value);
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
	        Label = Controls.Label;
	        Instruction = Controls.Instruction;
	        Argument = Controls.Number;
	        Comment = Controls.Comment;
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
	        if (!(sender is StatementWrapper statement)) return;

	        var index = _statements.IndexOf(statement);
	        _statements.Insert(index, StatementWrapper.GetEmptyInstance());
	    }

	    private void AddBelowExecute(object sender)
	    {
	        if (!(sender is StatementWrapper statement)) return;

	        var index = _statements.IndexOf(statement);
	        _statements.Insert(index + 1, StatementWrapper.GetEmptyInstance());
	    }

	    private void DeleteExecute(object sender)
	    {
	        if (!(sender is StatementWrapper statement)) return;
	        _statements.Remove(statement);

	        if (_statements.Count == 0)
	            _statements.Add(StatementWrapper.GetEmptyInstance());
	    }

	    private void ClearTapeExecute(object sender)
	    {
	        if (!(sender is StatementWrapper)) return;
	        _statements.Clear();
	        _statements.Add(StatementWrapper.GetEmptyInstance());
	    }

	    private static StatementWrapper _clipboard;

	    private void PasteExecute(object sender)
	    {
	        if (!(sender is StatementWrapper statement)) return;
	        if (_clipboard == null) return;

	        var index = _statements.IndexOf(statement);
	        _statements.Insert(index, _clipboard);
	    }

	    private static void CopyExecute(object sender)
	    {
	        if (!(sender is StatementWrapper statement)) return;
            _clipboard = new StatementWrapper(statement);
	    }

	    private void CutExecute(object sender)
	    {
	        if (!(sender is StatementWrapper statement)) return;

	        _statements.Remove(statement);
	        _clipboard = new StatementWrapper(statement);
	    }
        #endregion
    }
}