using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using Prism.Events;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Resources.MenuItems;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel.Menus
{
    public interface IMenuBarViewModel : IViewModel
    {
        string File { get; }
        string Edit { get; }
        string View { get; }
        string About { get; }

        ICommand TestCommand { get; }
        ICommand ChangeLanguageCommand { get; }
    }

    public class MenuBarViewModel : BaseViewModel, IMenuBarViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private string _about;
        private string _edit;

        private string _file;
        private string _view;

        public MenuBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            SeedCommands();
            LoadLocalizationStrings();
        }

        protected sealed override void SeedCommands()
        {
            TestCommand = new RelayCommand(Execute);

            ChangeLanguageCommand = new RelayCommand(ChangeLanguageExecute);
        }

        protected sealed override void LoadLocalizationStrings()
        {
            File = MenuItems.File;
            Edit = MenuItems.Edit;
            View = MenuItems.View;
            About = MenuItems.About;
        }

        #region Properties

        public string File
        {
            get => _file;
            protected set => SetProperty(ref _file, value);
        }

        public string Edit
        {
            get => _edit;
            protected set => SetProperty(ref _edit, value);
        }

        public string View
        {
            get => _view;
            protected set => SetProperty(ref _view, value);
        }

        public string About
        {
            get => _about;
            protected set => SetProperty(ref _about, value);
        }

        public ICommand TestCommand { get; protected set; }

        public ICommand ChangeLanguageCommand { get; protected set; }

        #endregion

        #region Command methods

        // Only for current testing purpose
        private void ChangeLanguageExecute(object sender)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
            _eventAggregator.GetEvent<LanguageChangedEvent>().Publish();
        }

        private static void Execute(object o)
        {
            // Only for current testing purpose
            var result = MessageBox.Show(@"Do you want to close this window?", @"Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();
        }

        #endregion
    }
}