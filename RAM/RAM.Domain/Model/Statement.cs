using System;
using RAM.Domain.Helpers;

namespace RAM.Domain.Model
{
	[Serializable]
	public class Statement
	{
		public Guid Id { get; protected set; }
		public string Label { get; protected set; }
		public string Instruction { get; protected set; }
		public string Argument { get; protected set; }
		public string Comment { get; protected set; }

	    public static Statement GetEmptyInstance()
	        => new Statement { Id = Guid.NewGuid() };

        #region Constructors

        public Statement(string instruction, string argument = "",
	        string label = "", string comment = "")
	    {
	        Id = Guid.NewGuid();
	        SetLabel(label);
	        SetInstruction(instruction);
	        SetArgument(argument);
	        SetComment(comment);
	    }

	    protected Statement()
	    {
	    }

        #endregion

        #region Setter methods

	    public void SetLabel(string label)
	    {
	        if (Label == label)
	            throw new Exception("You cannot save statement's label with the same value.");
	        Label = label;
	    }

	    public void SetInstruction(string instruction)
	    {
	        if (instruction.IsNullOrWhiteSpace())
	            throw new Exception("Statement's instruction cannot be empty.");
	        if (Instruction == instruction)
	            throw new Exception("You cannot save statement's instruction with the same value.");
	        Instruction = instruction;
	    }

	    public void SetArgument(string argument)
	    {
	        if (Argument == argument)
	            throw new Exception("You cannot save statement's argument with the same value.");
	        Argument = argument;
	    }

	    public void SetComment(string comment)
	    {
	        if (Comment == comment)
	            throw new Exception("You cannot save statement's comment with the same value.");
	        Comment = comment;
	    }

        #endregion
    }
}