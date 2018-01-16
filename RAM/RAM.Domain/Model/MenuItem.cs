using System;
using RAM.Domain.Helpers;

namespace RAM.Domain.Model
{
	[Serializable]
	public class MenuItem
	{
		public Guid Id { get; protected set; }
		public string Header { get; protected set; }
		public bool IsChecked { get; protected set; }

		public void SetHeader(string header)
		{
			if (header.IsNullOrWhiteSpace())
				throw new Exception("Menu item's header cannot be empty.");
			if (Header == header)
				throw new Exception("You cannot save menu item's header with the same value.");
			Header = header;
		}

		public void SetIsChecked(bool isChecked)
		{
			if (IsChecked == isChecked)
				throw new Exception("You cannot save menu item's is checked with the same value.");
			IsChecked = isChecked;
		}

		// Working perfectly fine, but in MVVM pattern is causing more troubles than benefits.
		[Obsolete]
		public void Check()
		{
			if (!IsChecked)
				throw new Exception("Menu item is already checked.");
			IsChecked = true;
		}

		[Obsolete]
		public void Uncheck()
		{
			if (!IsChecked)
				throw new Exception("Menu item is already unchecked.");
			IsChecked = false;
		}

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
	}
}