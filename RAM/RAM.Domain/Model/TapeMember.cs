using System;
using RAM.Domain.Helpers;

namespace RAM.Domain.Model
{
	public class TapeMember
	{
		public Guid Id { get; protected set; }
		public string Value { get; protected set; }
	    public int Number { get; protected set; } = -1;

        public static TapeMember GetEmptyInstance()
	        => new TapeMember();
	        
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
	            throw new Exception("You cannot save tape member's number with value less than zero.");
	        if (Number == number)
	            throw new Exception("You cannot save tape member's number with the same value.");
	        Number = number;
	    }

	    public void SetValue(string value)
	    {
	        if (Value == value)
	            throw new Exception("You cannot save tape member's value with the same value.");
	        Value = value;
	    }

        #endregion
    }
}