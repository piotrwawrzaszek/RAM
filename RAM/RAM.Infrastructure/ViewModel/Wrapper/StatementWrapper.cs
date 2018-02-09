using System;
using System.Runtime.CompilerServices;
using RAM.Domain.Model;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel.Wrapper
{
	public class StatementWrapper : BaseViewModel
	{
		private bool _isChanged;

	    #region Constructors

	    public StatementWrapper(string instruction, string argument = "",
	        string label = "", string comment = "")
	    {
	        Model = new Statement(instruction, argument, label, comment);
	    }

	    public StatementWrapper(Statement statement) : this(statement.Instruction, 
            statement.Argument, statement.Label, statement.Comment)
		{
		}

	    public StatementWrapper(StatementWrapper statementWrapper) 
            : this(statementWrapper.Model)
	    {
	    }

	    #endregion

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
			    if (Model.Label == value) return;
				Model.SetLabel(value);
				OnPropertyChanged();
			}
		}

		public string Instruction
		{
			get => Model.Instruction;
			set
			{
			    if (Model.Instruction == value) return;
                Model.SetInstruction(value);
				OnPropertyChanged();
			}
		}

		public string Argument
		{
			get => Model.Argument;
			set
			{
			    if (Model.Argument == value) return;
                Model.SetArgument(value);
				OnPropertyChanged();
			}
		}

		public string Comment
		{
			get => Model.Comment;
			set
			{
			    if (Model.Comment == value) return;
                Model.SetComment(value);
				OnPropertyChanged();
			}
		}

		#endregion
	}
}