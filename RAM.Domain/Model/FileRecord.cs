using System;
using System.Collections.Generic;
using RAM.Domain.Helpers.Extensions;
using RAM.Domain.Resources;

namespace RAM.Domain.Model
{
    public class FileRecord
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Comment { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public List<Statement> Statements { get; protected set; }
        public List<TapeMember> InputMembers { get; protected set; }

        public static FileRecord GetEmptyInstance()
        {
            return new FileRecord {Id = Guid.NewGuid()};
        }

        #region Constructors

        public FileRecord(Guid id, string name, string comment, IEnumerable<Statement> statements,
            IEnumerable<TapeMember> inputMembers)
        {
            Id = id;
            CreatedAt = DateTime.Now;
            SetName(name);
            SetComment(comment);
            SetStatements(statements);
            SetInputMembers(inputMembers);
        }

        public FileRecord(string name, string comment, IEnumerable<Statement> statements,
            IEnumerable<TapeMember> inputMembers)
        {
            Id = Guid.NewGuid();
            SetCreatedAt(DateTime.Now);
            SetName(name);
            SetComment(comment);
            SetStatements(statements);
            SetInputMembers(inputMembers);
        }

        public FileRecord(FileRecord fileRecord) : this(fileRecord.Name, fileRecord.Comment,
            fileRecord.Statements, fileRecord.InputMembers)
        {
        }

        public FileRecord(string name, string comment)
        {
            Id = Guid.NewGuid();
            SetCreatedAt(DateTime.Now);
            SetName(name);
            SetComment(comment);
        }

        protected FileRecord()
        {
        }

        #endregion

        #region Setter methods

        public void SetName(string name)
        {
            if (name.IsNullOrWhiteSpace())
                throw new Exception(ExceptionMessages.EmptyRecordName);
            if (Name == name)
                throw new Exception(ExceptionMessages.DuplicatedLabel);
            Name = name;
        }

        public void SetComment(string comment)
        {
            if (Comment == comment)
                throw new Exception(ExceptionMessages.FileRecord_DuplicatedComment);
            Comment = comment;
        }

        public void SetCreatedAt(DateTime createdAt)
        {
            if (createdAt > DateTime.Now)
                throw new Exception(ExceptionMessages.DateOutOfRange);
            CreatedAt = createdAt;
        }

        public void SetInputMembers(IEnumerable<TapeMember> inputMembers)
        {
            if (Equals(InputMembers, inputMembers))
                throw new Exception(ExceptionMessages.DuplicatedInputMembers);
            InputMembers = new List<TapeMember>(inputMembers);
        }

        public void SetStatements(IEnumerable<Statement> statements)
        {
            if (Equals(Statements, statements))
                throw new Exception(ExceptionMessages.DuplicatedStatements);
            Statements = new List<Statement>(statements);
        }

        #endregion
    }
}