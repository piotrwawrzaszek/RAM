using System.Globalization;
using RAM.Infrastructure.ViewModel.Base;
using System.Threading;
using System.Configuration;
using Prism.Events;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Resources.Controls;

namespace RAM.Infrastructure.ViewModel
{
	public interface IMainWindowViewModel : IViewModel
	{
		string Header { get; set; }
	}

	public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
	{
	    private string _header;
        
        public MainWindowViewModel(IEventAggregator eventAggregator, 
            ICultureInfoProvider cultureInfoProvider)
        {
            eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            Thread.CurrentThread.CurrentUICulture = cultureInfoProvider.GetCultureInfo();
            LoadLocalizationStrings();
        }

		public string Header
		{
			get => _header;
			set => SetProperty(ref _header, value);
		}

        #region Event handlers

	    private void LoadLocalizationStrings()
	    {
	        Header = Controls.Title;
	    }

        #endregion
    }
}