using System.Configuration;
using System.Globalization;

namespace RAM.Infrastructure.Data
{
    public interface IConfigurationProvider : IDataProvider
    {
        CultureInfo GetCultureInfo();
        string GetStorageFilePath();
        string GetExamplesFilePath();
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        public CultureInfo GetCultureInfo()
            => new CultureInfo(ConfigurationManager.AppSettings["Culture"]);

        public string GetStorageFilePath()
            => ConfigurationManager.AppSettings["SaveFilePath"];

        public string GetExamplesFilePath()
            => ConfigurationManager.AppSettings["ExamplesFilePath"];
    }
}