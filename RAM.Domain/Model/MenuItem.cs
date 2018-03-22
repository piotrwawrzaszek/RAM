using System;
using RAM.Domain.Helpers.Extensions;
using RAM.Domain.Resources;

namespace RAM.Domain.Model
{
	public class MenuItem
	{
		public Guid Id { get; protected set; }
		public string Header { get; protected set; }
		public bool IsChecked { get; protected set; }

	    public static MenuItem GetEmptyInstance()
	        => new MenuItem { Id = Guid.NewGuid() };

        #region Constructors

        public MenuItem(Guid id, string header, bool isChecked)
	    {
	        Id = id;
	        SetHeader(header);
	        IsChecked = isChecked;
	    }

	    public MenuItem(string header)
	    {
	        Id = Guid.NewGuid();
	        SetHeader(header);
	        IsChecked = false;
	    }

	    protected MenuItem()
	    {
	    }

        #endregion
        
        #region Setter methods

	    public void SetHeader(string header)
	    {
	        if (header.IsNullOrWhiteSpace())
	            throw new Exception(ExceptionMessages.EmptyHeader);
            if (Header == header)
                throw new Exception(ExceptionMessages.DuplicatedHeader);
            Header = header;
	    }

	    public void SetIsChecked(bool isChecked)
	    {
	        if (IsChecked == isChecked)
	            throw new Exception(ExceptionMessages.DuplicatedIsChecked);
	        IsChecked = isChecked;
	    }
        
	    /// Working perfectly fine, but in MVVM pattern is causing more troubles than benefits.
	    [Obsolete("Use SetIsChecked method instead.")]
	    public void Check()
	    {
	        if (!IsChecked)
	            throw new Exception(ExceptionMessages.AlreadyChecked);
	        IsChecked = true;
	    }

	    [Obsolete("Use SetIsChecked method instead.")]
	    public void Uncheck()
	    {
	        if (!IsChecked)
	            throw new Exception(ExceptionMessages.AlreadyUnchecked);
	        IsChecked = false;
	    }

        #endregion
    }
}