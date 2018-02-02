using System;
using RAM.Domain.Helpers;

namespace RAM.Domain.Model
{
	[Serializable]
	public class TapeMember
	{
		public Guid Id { get; protected set; }
		public int Number { get; protected set; }
		public string Value { get; protected set; }

        public static TapeMember GetEmptyInstance()
	        => new TapeMember {Id = Guid.NewGuid()};

	    #region Constructors

	    public TapeMember(int number, string value)
	    {
	        Id = Guid.NewGuid();
	        SetNumber(number);
	        SetValue(value);
	    }

	    public TapeMember(string value)
	    {
	        Id = Guid.NewGuid();
	        SetValue(value);
	    }

	    protected TapeMember()
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
	        if (value.IsNullOrEmpty())
	            throw new Exception("Tape member's value cannot be empty.");
	        if (Value == value)
	            throw new Exception("You cannot save tape member's value with the same value.");
	        Value = value;
	    }

        #endregion
    }
}