using System.Collections.ObjectModel;
using System.Linq;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
	public interface IStatementGridViewModel : IViewModel
	{
		StatementWrapper SelectedStatement { get; set; }
		ObservableCollection<StatementWrapper> Statements { get; set; }
	}

	public class StatementGridViewModel : BaseViewModel, IStatementGridViewModel
	{
		private readonly IStatementProvider _statementProvider;

		private StatementWrapper _selectedStatement;
		private ObservableCollection<StatementWrapper> _statements;

		public StatementGridViewModel(IStatementProvider statementProvider)
		{
			_statementProvider = statementProvider;

			_statements = new ObservableCollection<StatementWrapper>(
				_statementProvider.GetAllStatements().Select(x => new StatementWrapper(x)));
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
	}
}