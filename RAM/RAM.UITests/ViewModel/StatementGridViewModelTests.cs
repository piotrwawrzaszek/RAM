using System.Collections.ObjectModel;
using FluentAssertions;
using Moq;
using Prism.Events;
using RAM.Domain.Model;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Wrapper;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class StatementGridViewModelTests
	{
		public StatementGridViewModelTests()
		{
			_statementProviderMock = new Mock<IStatementProvider>();
		    _eventAggregatorMock = new Mock<IEventAggregator>();

		    var languageChangedEventMock = new Mock<LanguageChangedEvent>();
		    _eventAggregatorMock
		        .Setup(ea => ea.GetEvent<LanguageChangedEvent>())
		        .Returns(languageChangedEventMock.Object);

            _statementGridViewModel = new StatementGridViewModel(_statementProviderMock.Object, _eventAggregatorMock.Object);
		}

		private readonly IStatementGridViewModel _statementGridViewModel;

	    private readonly Mock<IEventAggregator> _eventAggregatorMock;
        private readonly Mock<IStatementProvider> _statementProviderMock;

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