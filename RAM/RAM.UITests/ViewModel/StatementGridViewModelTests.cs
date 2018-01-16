using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RAM.Domain.Model;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Wrapper;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class StatementGridViewModelTests
	{
		private readonly IStatementGridViewModel _statementGridViewModel;
		private readonly Mock<IStatementProvider> _statementProviderMock;

		public StatementGridViewModelTests()
		{
			_statementProviderMock = new Mock<IStatementProvider>();
			_statementGridViewModel = new StatementGridViewModel(_statementProviderMock.Object);
		}

		[Fact]
		public void Should_raise_property_changed_event_for_selected_statement()
		{
			_statementGridViewModel.MonitorEvents();
			_statementGridViewModel.SelectedStatement = new StatementWrapper(new Statement("HALT"));
			_statementGridViewModel.ShouldRaisePropertyChangeFor(x => x.SelectedStatement);
		}

		[Fact]
		public void Should_raise_property_changed_event_for_statements()
		{
			_statementGridViewModel.MonitorEvents();
			_statementGridViewModel.Statements = new ObservableCollection<StatementWrapper>();
			_statementGridViewModel.ShouldRaisePropertyChangeFor(x => x.Statements);
		}
	}
}

