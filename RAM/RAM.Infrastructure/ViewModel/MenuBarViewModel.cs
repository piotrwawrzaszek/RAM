using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using Prism.Events;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.Resources.MenuItems;
using static RAM.Infrastructure.ViewModel.MenuItemViewModel;

namespace RAM.Infrastructure.ViewModel
{
    public interface IMenuBarViewModel : IViewModel
    {
        ICommand ChangeLanguageCommand { get; }
        ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; }
    }

    public class MenuBarViewModel : BaseViewModel, IMenuBarViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public MenuBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ChangeLanguageCommand = new RelayCommand(ChangeLanguageExecute);

            MenuItemViewModels = Seed();
        }

        public ICommand ChangeLanguageCommand { get; protected set; }
        public ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; protected set; }

        #region Command methods

        // Only for current testing purpose
        private void ChangeLanguageExecute(object o)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
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

        private ObservableCollection<IMenuItemViewModel> Seed()
        {
            var menuItems = new ObservableCollection<IMenuItemViewModel>
            {
                LoadInstance(() => MenuItems.File, null,
                    new List<IMenuItemViewModel>
                    {
                        LoadInstance(() => "Option 1", new RelayCommand(Execute)),
                        LoadInstance(() => "Option 2", new RelayCommand(Execute))
                    }),
                LoadInstance(() => MenuItems.Edit, null,
                    new List<IMenuItemViewModel>
                    {
                        LoadInstance(() => "Option 1", new RelayCommand(Execute)),
                        LoadInstance(() => "Option 2", new RelayCommand(Execute)),
                        LoadInstance(()=> "de-DE", ChangeLanguageCommand)
                    }),
                LoadInstance(() => MenuItems.View, null,
                    new List<IMenuItemViewModel>
                    {
                        LoadInstance(() => "Option 1", new RelayCommand(Execute)),
                        LoadInstance(() => "Option 2", new RelayCommand(Execute))
                    }),
                LoadInstance(() => MenuItems.About, null,
                    new List<IMenuItemViewModel>
                    {
                        LoadInstance(() => "Option 1", new RelayCommand(Execute)),
                        LoadInstance(() => "Option 2", new RelayCommand(Execute))
                    })
            };
            return menuItems;
        }

        #endregion
    }
}