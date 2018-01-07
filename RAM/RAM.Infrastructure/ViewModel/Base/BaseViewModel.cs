using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RAM.Infrastructure.Annotations;

namespace RAM.Infrastructure.ViewModel.Base
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		#region SetProperty

		protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value))
				return false;
			storage = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		#endregion
		
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}