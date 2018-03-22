using System;
using RAM.Domain.Helpers;
using RAM.Domain.Resources;

namespace RAM.Domain.Model
{
	public class Statement
	{
		public Guid Id { get; protected set; }
		public string Label { get; protected set; }
		public Instruction Instruction { get; protected set; } 
		public string Argument { get; protected set; } 
		public string Comment { get; protected set; }

	    public static Statement GetEmptyInstance()
	        => new Statement();

        #region Constructors

        public Statement(Instruction instruction = Instruction.Empty, 
            string argument = "", string label = "", string comment = "")
	    {
	        Id = Guid.NewGuid();
	        SetLabel(label);
	        SetInstruction(instruction);
	        SetArgument(argument);
	        SetComment(comment);
	    }

	    public Statement(Statement statement) : this(statement.Instruction, 
            statement.Argument, statement.Label, statement.Comment)
	    {
	    }

        #endregion

        #region Setter methods

	    public void SetLabel(string label)
	    {
	        if (Label == label)
	            throw new Exception(ExceptionMessages.DuplicatedLabel);
	        Label = label;
	    }

	    public void SetInstruction(Instruction instruction)
	    {
	        if (Instruction == instruction)
	            throw new Exception(ExceptionMessages.DuplicatedInstruction);
	        Instruction = instruction;
	    }

	    public void SetArgument(string argument)
	    {
	        if (Argument == argument)
	            throw new Exception(ExceptionMessages.DuplicatedArgument);
	        Argument = argument;
	    }

	    public void SetComment(string comment)
	    {
	        if (Comment == comment)
	            throw new Exception(ExceptionMessages.Statement_DuplicatedComment);
	        Comment = comment;
	    }

        #endregion
    }
}