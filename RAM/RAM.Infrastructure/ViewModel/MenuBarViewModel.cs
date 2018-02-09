using System.Collections.Generic;
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

namespace RAM.Infrastructure.ViewModel
{
    public interface IMenuBarViewModel : IViewModel
    {
        ICommand TestCommand { get; }

        ICommand ChangeLanguageCommand { get; }
        ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; }
    }

    public class MenuBarViewModel : BaseViewModel, IMenuBarViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public MenuBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            SeedCommands();
            SeedMenuItems();
        }

        #region Properties

        public ICommand TestCommand { get; protected set; }

        public ICommand ChangeLanguageCommand { get; protected set; }
        public ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; protected set; }

        #endregion

        #region Command methods

        // Only for current testing purpose
        private void ChangeLanguageExecute(object o)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
            _eventAggregator.GetEvent<LanguageChangedEvent>().Publish();
        }

        private void Execute(object o)
        {
            // Only for current testing purpose
            var result = MessageBox.Show(@"Do you want to close this window?", @"Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();
        }

        #endregion

        #region Menu items creation

        private void SeedCommands()
        {
            TestCommand = new RelayCommand(Execute);

            ChangeLanguageCommand = new RelayCommand(ChangeLanguageExecute);
        }

        private void SeedMenuItems()
        {
            MenuItemViewModels = new ObservableCollection<IMenuItemViewModel>
            {
                new MenuItemViewModel(() => MenuItems.File, _eventAggregator).Load(null, 
                new List<IMenuItemViewModel>
                    {
                        new MenuItemViewModel(() => "Lang: EN", _eventAggregator).Load(ChangeLanguageCommand),
                        new MenuItemViewModel(() => "Option 2", _eventAggregator).Load(TestCommand)
                    }),
                new MenuItemViewModel(() => MenuItems.Edit, _eventAggregator)
                    .Load(null, new List<IMenuItemViewModel>
                    {
                        new MenuItemViewModel(() => "Option 1", _eventAggregator).Load(TestCommand),
                        new MenuItemViewModel(() => "Option 2", _eventAggregator).Load(TestCommand)
                    }),
                new MenuItemViewModel(() => MenuItems.View, _eventAggregator)
                    .Load(null, new List<IMenuItemViewModel>
                    {
                        new MenuItemViewModel(() => "Option 1", _eventAggregator).Load(TestCommand),
                        new MenuItemViewModel(() => "Option 2", _eventAggregator).Load(TestCommand)
                    }),
                new MenuItemViewModel(() => MenuItems.About, _eventAggregator)
                    .Load(null, new List<IMenuItemViewModel>
                    {
                        new MenuItemViewModel(() => "Option 1", _eventAggregator).Load(TestCommand),
                        new MenuItemViewModel(() => "Option 2", _eventAggregator).Load(TestCommand)
                    })
            };
        }

        #endregion
    }
}