using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RAM.Domain.Helpers.Extensions;
using RAM.Domain.Model;
using RAM.Domain.Resources;

namespace RAM.Infrastructure.Services
{
    public interface IFileDataService : IService, IDisposable
    {
        string FilePath { get; set; }
        FileRecord GetRecordById(Guid recordId);
        void SaveRecord(FileRecord record);
        void DeleteRecord(Guid recordId);
        IEnumerable<FileRecord> GetAllRecords();
    }

    public class FileDataService : IFileDataService
    {
        #region IDisposable implementation

        public void Dispose()
        {
        }

        #endregion

        #region IFileDataService implementation

        public string FilePath { get; set; }

        public FileRecord GetRecordById(Guid recordId)
            => ReadFromFile().SingleOrDefault(x => x.Id == recordId);    

        public void SaveRecord(FileRecord record)
        {
            var records = GetAllRecords().ToList();

            if (records.Any(x => x.Id == record.Id))
                records[records.IndexOf(record)] = record;
            else
                records.Add(record);
            SaveToFile(records);
        }

        public void DeleteRecord(Guid recordId)
        {
            var records = ReadFromFile().ToList();
            records.Remove(GetRecordById(recordId));
            SaveToFile(records);
        }

        public IEnumerable<FileRecord> GetAllRecords()
            => ReadFromFile();

        #endregion

        #region private methods

        private IEnumerable<FileRecord> ReadFromFile()
        {
            if (FilePath.IsNullOrEmpty())
            {
                throw new Exception(ExceptionMessages.EmptyPath);
            }
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
                return new List<FileRecord>();
            }
            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<FileRecord>>(json);
        }

        private void SaveToFile(List<FileRecord> records)
        {
            if (FilePath.IsNullOrEmpty())
            {
                throw new Exception(ExceptionMessages.EmptyPath);
            }
            var json = JsonConvert.SerializeObject(records);
            File.WriteAllText(FilePath, json);
        }

        #endregion
    }
}