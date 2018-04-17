using System;
using System.Collections.Generic;
using System.Linq;
using RAM.Domain.Helpers;
using RAM.Domain.Model;
using RAM.Infrastructure.Services;

namespace RAM.Infrastructure.Data
{
	public interface IStatementProvider : IDataProvider
	{
		Statement GetStatement(Guid statementId);
	    //void Save();
	    //void Delete();
		IEnumerable<Statement> GetAllStatements();
	}

	public class StatementProvider : IStatementProvider
	{
	    private readonly IFileDataService _fileDataService;

		private readonly IEnumerable<Statement> _statements;

		public StatementProvider()
		{
			_statements = new List<Statement>
			{
				new Statement(Instruction.READ, "1", "start:"),
				new Statement(Instruction.READ, "2"),
				new Statement(Instruction.ADD, "=2"),
				new Statement(Instruction.WRITE, "1"),
				new Statement(Instruction.HALT),
				new Statement()
			};
		}

		public Statement GetStatement(Guid statementId)
			=> _statements.SingleOrDefault(x => x.Id == statementId);

		public IEnumerable<Statement> GetAllStatements()
			=> _statements;
	}
}