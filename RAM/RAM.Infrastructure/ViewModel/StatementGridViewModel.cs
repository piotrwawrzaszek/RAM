using System.Collections.ObjectModel;
using System.Linq;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;
using static RAM.Infrastructure.ViewModel.MenuItemViewModel;

namespace RAM.Infrastructure.ViewModel
{
	public interface IStatementGridViewModel : IViewModel
	{
		StatementWrapper SelectedStatement { get; set; }
		ObservableCollection<StatementWrapper> Statements { get; set; }
	    ObservableCollection<IMenuItemViewModel> MenuItems { get; }
    }

    public class StatementGridViewModel : BaseViewModel, IStatementGridViewModel
	{
		private readonly IStatementProvider _statementProvider;

		private StatementWrapper _selectedStatement;
		private ObservableCollection<StatementWrapper> _statements;


	    public StatementGridViewModel(IStatementProvider statementProvider)
		{
			_statementProvider = statementProvider;

		    MenuItems = Seed();
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

	    public ObservableCollection<IMenuItemViewModel> MenuItems { get; protected set; }


	    #region Menu items creation

        private ObservableCollection<IMenuItemViewModel> Seed()
	    {
	        var menuItems = new ObservableCollection<IMenuItemViewModel>
	        {
	            LoadInstance(Resources.MenuItems.PasteEN, new RelayCommand(PasteExecute)),
	            LoadInstance(Resources.MenuItems.CopyEN, new RelayCommand(CopyExecute)),
	            LoadInstance(Resources.MenuItems.CutEN, new RelayCommand(CutExecute)),
                LoadInstance(Resources.MenuItems.AddAboveEN, new RelayCommand(AddAboveExecute)),
	            LoadInstance(Resources.MenuItems.AddBelowEN, new RelayCommand(AddBelowExecute)),
	            LoadInstance(Resources.MenuItems.DeleteEN, new RelayCommand(DeleteExecute)),
	            LoadInstance(Resources.MenuItems.ClearTapeEN, new RelayCommand(ClearTapeExecute))
	        };
	        return menuItems;
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