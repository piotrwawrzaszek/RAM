using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RAM.Domain.Model;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel.Wrapper
{
    public class FileRecordWrapper : ViewModelBase, IWrapper
    {
        private bool _isChanged;

        public FileRecordWrapper(FileRecord fileRecord)
        {
            Model = fileRecord;
        }

        public FileRecord Model { get; }

        public static FileRecordWrapper GetEmptyInstance()
        {
            return new FileRecordWrapper(FileRecord.GetEmptyInstance());
        }

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

        #region FileRecord properties 

        public bool IsChanged
        {
            get => _isChanged;
            private set => SetProperty(ref _isChanged, value);
        }

        public Guid Id => Model.Id;

        public string Name
        {
            get => Model.Name;
            set
            {
                if (Model.Name == value) return;
                Model.SetName(value);
                OnPropertyChanged();
            }
        }

        public DateTime CreatedAt
        {
            get => Model.CreatedAt;
            set
            {
                if (Model.CreatedAt == value) return;
                Model.SetCreatedAt(value);
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

        public IEnumerable<Statement> Statements
        {
            get => Model.Statements;
            set
            {
                Model.SetStatements(value);
                OnPropertyChanged();
            }
        }

        public IEnumerable<TapeMember> InputMembers
        {
            get => Model.InputMembers;
            set
            {
                Model.SetInputMembers(value);
                OnPropertyChanged();
            }
        }

        #endregion
    }
}