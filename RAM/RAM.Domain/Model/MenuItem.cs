using System;

namespace RAM.Domain.Model
{
	[Serializable]
	public class MenuItem
	{
		#region Constructors

		public MenuItem(Guid id, string header, bool isChecked)
		{
			Id = id;
			Header = header;
			IsChecked = isChecked;
		}

		public MenuItem(string header)
		{
			Id = Guid.NewGuid();
			Header = header;
			IsChecked = false;
		}

		protected MenuItem()
		{
		}

		#endregion

		public Guid Id { get; protected set; }
		public string Header { get; protected set; }
		public bool IsChecked { get; protected set; }

		public void SetHeader(string header)
		{
			if (string.IsNullOrWhiteSpace(header)) return;
			if (Header == header) return;
			Header = header;
		}

		public void Check()
		{
			if (!IsChecked) return;
			IsChecked = true;
		}

		public void Uncheck()
		{
			if (!IsChecked) return;
			IsChecked = false;
		}
	}
}