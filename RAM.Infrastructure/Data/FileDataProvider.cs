using System;
using System.Collections.ObjectModel;
using System.Linq;
using RAM.Domain.Helpers;
using RAM.Infrastructure.Services;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.Data
{
    public interface IFileDataProvider : IDataProvider
    {
        FilePath FilePath { get; set; }
        FileRecordWrapper GetRecordById(Guid recordId);
        void SaveRecord(FileRecordWrapper record);
        void DeleteRecord(Guid recordId);
        ObservableCollection<FileRecordWrapper> GetAllRecords();
    }

    public class FileDataProvider : IFileDataProvider
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly Func<IFileDataService> _fileDataServiceCreator;

        public FileDataProvider(Func<IFileDataService> fileDataServiceCreator,
            IConfigurationProvider configurationProvider)
        {
            _fileDataServiceCreator = fileDataServiceCreator;
            _configurationProvider = configurationProvider;
        }

        public FilePath FilePath { get; set; }

        public FileRecordWrapper GetRecordById(Guid recordId)
        {
            using (var fileDataService = _fileDataServiceCreator())
            {
                fileDataService.FilePath = ResolveFilePath(FilePath);
                return new FileRecordWrapper(fileDataService.GetRecordById(recordId));
            }
        }

        public void SaveRecord(FileRecordWrapper record)
        {
            using (var fileDataService = _fileDataServiceCreator())
            {
                fileDataService.FilePath = ResolveFilePath(FilePath);
                fileDataService.SaveRecord(record.Model);
            }
        }

        public void DeleteRecord(Guid recordId)
        {
            using (var fileDataService = _fileDataServiceCreator())
            {
                fileDataService.FilePath = ResolveFilePath(FilePath);
                fileDataService.DeleteRecord(recordId);
            }
        }

        public ObservableCollection<FileRecordWrapper> GetAllRecords()
        {
            using (var fileDataService = _fileDataServiceCreator())
            {
                fileDataService.FilePath = ResolveFilePath(FilePath);
                return new ObservableCollection<FileRecordWrapper>(fileDataService.GetAllRecords()
                    .Select(x => new FileRecordWrapper(x)));
            }
        }

        private string ResolveFilePath(FilePath filePath)
        {
            string result;
            switch (filePath)
            {
                case FilePath.Storage:
                    result = _configurationProvider.GetStorageFilePath();
                    break;
                case FilePath.Examples:
                    result = _configurationProvider.GetExamplesFilePath();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filePath), filePath, null);
            }

            return result;
        }
    }
}