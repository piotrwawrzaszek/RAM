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

        public static MenuItemWrapper GetEmptyInstance()
            => new MenuItemWrapper(MenuItem.GetEmptyInstance());

		public MenuItem Model { get; }

		public void AcceptChanges()
		{
			IsChanged = false;
		}

		#region NotifyOnpropertyChanged override

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName != nameof(IsChanged))
				IsChanged = true;
		}

		#endregion

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
			    if (Model.Header == value) return;
                Model.SetHeader(value);
				OnPropertyChanged();
			}
		}

		public bool IsChecked
		{
			get => Model.IsChecked;
			set
			{
				Model.SetIsChecked(value);
				OnPropertyChanged();
			}
		}

		#endregion
	}
}