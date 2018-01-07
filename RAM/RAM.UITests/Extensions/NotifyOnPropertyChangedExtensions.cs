using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.UITests.Extensions
{
	public static class NotyifyOnPropertyChangedExtensions
	{
		public static bool IsPropertyChangedFired(this INotifyPropertyChanged property,
			Action action, string propertyName)
		{
			var fired = false;
			property.PropertyChanged += (sender, args) =>
			{
				fired = args.PropertyName == propertyName;
			};

			action();
			return fired;
		}
	}
}
