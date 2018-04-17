using System.Runtime.CompilerServices;
using RAM.Domain.Model;
using RAM.Infrastructure.ViewModel.Base;
using System;

namespace RAM.Infrastructure.ViewModel.Wrapper
{
	public class TapeMemberWrapper : ViewModelBase, IWrapper
    {
		private bool _isChanged;

	    #region Constructors

	     public TapeMemberWrapper(int number, string value)
	    {
	        Model = new TapeMember(number, value);
	    }

        public TapeMemberWrapper(TapeMember tapeMember) 
            : this(tapeMember.Number, tapeMember.Value)
        {
		}

	    public TapeMemberWrapper(TapeMemberWrapper tapeMemberWrapper) 
            : this(tapeMemberWrapper.Model)
	    {
	    }

        public TapeMemberWrapper()
        {
            Model = new TapeMember();
        }

        #endregion
       
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
			    if (Model.Number == value) return;
                Model.SetNumber(value);
				OnPropertyChanged();
			}
		}

		public string Value
		{
			get => Model.Value;
			set
			{
			    if (Model.Value == value) return;
                Model.SetValue(value);
				OnPropertyChanged();
			}
		}

		#endregion
	}
}