using System.Configuration;
using System.Globalization;

namespace RAM.Infrastructure.Data
{
    public interface ICultureInfoProvider : IDataProvider
    {
        CultureInfo GetCultureInfo();
    }

    public class CultureInfoProvider : ICultureInfoProvider
    {
        public CultureInfo GetCultureInfo()
            => new CultureInfo(ConfigurationManager.AppSettings["Culture"]);
    }
}