using System;
using System.Collections.Generic;
using System.Linq;
using RAM.Domain.Model;

namespace RAM.Infrastructure.Data
{
	public interface IStatementProvider : IDataProvider
	{
		Statement GetStatement(Guid statementId);
		IEnumerable<Statement> GetAllStatements();
	}

	public class StatementProvider : IStatementProvider
	{
		private readonly IEnumerable<Statement> _statements;

		public StatementProvider()
		{
			_statements = new List<Statement>
			{
				new Statement("READ", "1"),
				new Statement("READ", "2"),
				new Statement("ADD", "=2"),
				new Statement("WRITE", "1"),
				new Statement("HALT"),
				Statement.GetEmptyInstance()
			};
		}

		public Statement GetStatement(Guid statementId)
			=> _statements.SingleOrDefault(x => x.Id == statementId);

		public IEnumerable<Statement> GetAllStatements()
			=> _statements;
	}
}