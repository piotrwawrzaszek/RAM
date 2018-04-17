using RAM.Infrastructure.ViewModel.Base;
using System.Threading;
using Prism.Events;
using RAM.Infrastructure.Data;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Resources.Controls;

namespace RAM.Infrastructure.ViewModel
{
	public interface IMainWindowViewModel : IViewModel
	{
		string Title { get; }
	}

	public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{
        public MainWindowViewModel(IEventAggregator eventAggregator, 
            IConfigurationProvider configurationProvider)
        {
            eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            Thread.CurrentThread.CurrentUICulture = configurationProvider.GetCultureInfo();
            LoadLocalizationStrings();
        }

        #region Properties

        private string _title;

	    public string Title
	    {
	        get => _title;
	        protected set => SetProperty(ref _title, value);
	    }

        #endregion

        #region Event handlers

        protected sealed  override void LoadLocalizationStrings()
	    {
	        Title = Controls.Title;
	    }

        #endregion
    }
}