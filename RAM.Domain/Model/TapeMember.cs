using System;
using RAM.Domain.Resources;

namespace RAM.Domain.Model
{
	public class TapeMember
	{
		public Guid Id { get; protected set; }
		public string Value { get; protected set; }
	    public int Number { get; protected set; } = -1;
	        
	    #region Constructors

	    public TapeMember(int number, string value = "")
	    {
	        Id = Guid.NewGuid();
	        SetNumber(number);
	        SetValue(value);
	    }

	    public TapeMember(string value = "") : this(0, value)
	    {
	    }

	    public TapeMember(TapeMember tapeMember) 
            : this(tapeMember.Number, tapeMember.Value)
	    {
	    }

	    #endregion

        #region Setter methods

	    public void SetNumber(int number)
	    {
	        if (number < 0)
	            throw new Exception(ExceptionMessages.NumberLessThanZero);
	        if (Number == number)
	            throw new Exception(ExceptionMessages.DuplicatedNumber);
	        Number = number;
	    }

	    public void SetValue(string value)
	    {
	        if (Value == value)
	            throw new Exception(ExceptionMessages.DuplicatedValue);
	        Value = value;
	    }

        #endregion
    }
}