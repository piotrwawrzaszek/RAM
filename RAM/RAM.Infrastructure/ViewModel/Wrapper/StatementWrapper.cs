using System;
using System.Runtime.CompilerServices;
using RAM.Domain.Model;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel.Wrapper
{
   [Serializable]
	public class StatementWrapper : BaseViewModel
	{
		private bool _isChanged;

		public StatementWrapper(Statement statement)
		{
			Model = statement;
		}

        public static StatementWrapper GetEmptyInstance()
            => new StatementWrapper(Statement.GetEmptyInstance());
        
        public Statement Model { get; }

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

		#region Statement properties

		public bool IsChanged
		{
			get => _isChanged;
			private set => SetProperty(ref _isChanged, value);
		}

		public Guid Id => Model.Id;

		public string Label
		{
			get => Model.Label;
			set
			{
				Model.SetLabel(value);
				OnPropertyChanged();
			}
		}

		public string Instruction
		{
			get => Model.Instruction;
			set
			{
				Model.SetInstruction(value);
				OnPropertyChanged();
			}
		}

		public string Argument
		{
			get => Model.Argument;
			set
			{
				Model.SetArgument(value);
				OnPropertyChanged();
			}
		}

		public string Comment
		{
			get => Model.Comment;
			set
			{
				Model.SetComment(value);
				OnPropertyChanged();
			}
		}

		#endregion
	}
}