using System.Globalization;
using RAM.Infrastructure.ViewModel.Base;
using System.Threading;
using System.Configuration;
using RAM.Infrastructure.Resources.MenuItems;

namespace RAM.Infrastructure.ViewModel
{
	public interface IMainWindowViewModel : IViewModel
	{
		string Header { get; set; }
	}

	public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
	{
		private string _header;
	    private string _culture;
        
        public MainWindowViewModel()
		{
            _culture = ConfigurationManager.AppSettings["Culture"];
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_culture);

            _header = MenuItems.About;
		}

		public string Header
		{
			get => _header;
			set => SetProperty(ref _header, value);
		}
	}
}