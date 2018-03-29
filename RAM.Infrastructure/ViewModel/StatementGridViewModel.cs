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

namespace RAM.Infrastructure.ViewModel
{
	public interface IStatementGridViewModel : IViewModel
    {
	    string Label { get; }
	    string Instruction { get; }
	    string Argument { get; }
	    string Comment { get; }

        StatementWrapper SelectedStatement { get; set; }
		ObservableCollection<StatementWrapper> Statements { get; set; }
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
        
	    public StatementGridViewModel(IStatementProvider statementProvider, 
	        IEventAggregator eventAggregator) 
		{
			_statementProvider = statementProvider;
		    _eventAggregator = eventAggregator;
		    _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            
			_statements = new ObservableCollection<StatementWrapper>(
				_statementProvider.GetAllStatements().Select(x => new StatementWrapper(x)));
            LoadLocalizationStrings();
		}

        #region Properties

	    public string Label
	    {
	        get => _label;
	        protected set => SetProperty(ref _label, value);
	    }

	    public string Instruction
	    {
	        get => _instruction;
	        protected set => SetProperty(ref _instruction, value);
	    }

	    public string Argument
	    {
	        get => _argument;
	        protected set => SetProperty(ref _argument, value);
	    }

	    public string Comment
	    {
	        get => _comment;
	        protected set => SetProperty(ref _comment, value);
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

	    protected sealed override void LoadLocalizationStrings()
	    {
	        Label = Controls.Label;
	        Instruction = Controls.Instruction;
	        Argument = Controls.Argument;
	        Comment = Controls.Comment;
	    }

	    #endregion
    }
}