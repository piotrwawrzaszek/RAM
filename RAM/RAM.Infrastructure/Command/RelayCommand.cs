﻿using System;
using System.Windows.Input;
using RAM.Infrastructure.Annotations;

namespace RAM.Infrastructure.Command
{
	public class RelayCommand : ICommand
	{
		private readonly Func<object, bool> _canExecute;
		private readonly Action<object> _execute;

		public RelayCommand(
			[NotNull] Action<object> execute,
			[CanBeNull] Func<object, bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}
		
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
			=> _canExecute == null || _canExecute(parameter);

		public void Execute(object parameter)
			=> _execute(parameter);

		public void RaiseCanExecuteChanged()
			=> CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
