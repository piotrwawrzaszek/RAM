using System.Runtime.CompilerServices;
using RAM.Domain.Model;
using RAM.Infrastructure.ViewModel.Base;
using System;

namespace RAM.Infrastructure.ViewModel.Wrapper
{
    [Serializable]
	public class TapeMemberWrapper : BaseViewModel
	{
		private bool _isChanged;

		public TapeMemberWrapper(TapeMember tapeMember)
		{
			Model = tapeMember;
		}

        public static TapeMemberWrapper GetEmptyInstance()
            => new TapeMemberWrapper(TapeMember.GetEmptyInstance());
        
        public TapeMember Model { get; }

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

		#region Tape member properties

		public bool IsChanged
		{
			get => _isChanged;
			private set => SetProperty(ref _isChanged, value);
		}

		public Guid Id => Model.Id;

		public int Number
		{
			get => Model.Number;
			set
			{
				Model.SetNumber(value);
				OnPropertyChanged();
			}
		}

		public string Value
		{
			get => Model.Value;
			set
			{
				Model.SetValue(value);
				OnPropertyChanged();
			}
		}

		#endregion
	}
}