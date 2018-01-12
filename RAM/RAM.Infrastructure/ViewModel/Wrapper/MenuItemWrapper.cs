using System;
using System.Runtime.CompilerServices;
using RAM.Domain.Model;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel.Wrapper
{
	public class MenuItemWrapper : BaseViewModel
	{
		private bool _isChanged;

		public MenuItemWrapper(MenuItem menuItem)
		{
			Model = menuItem;
		}

		public MenuItem Model { get; }

		public void AcceptChanges()
		{
			IsChanged = false;
		}

		#region MenuItem properties 

		public bool IsChanged
		{
			get => _isChanged;
			private set => SetProperty(ref _isChanged, value);
		}

		public Guid Id => Model.Id;

		public string Header
		{
			get => Model.Header;
			set
			{
				Model.SetHeader(value);
				OnPropertyChanged();
			}
		}

		public bool IsChecked
		{
			get => Model.IsChecked;
			set
			{
				if (Model.IsChecked == value) return;
				if (Model.IsChecked) Model.Uncheck();
				else Model.Check();
				OnPropertyChanged();
			}
		}

		#endregion

		#region NotifyOnpropertyChanged override

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName != nameof(IsChanged))
				IsChanged = true;
		}

		#endregion
	}
}