using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel
{
    public interface IMenuBarViewModel : IViewModel
    {
        ObservableCollection<IMenuItemViewModel> MenuItems { get; }
    }

    public class MenuBarViewModel : BaseViewModel, IMenuBarViewModel
    {
        public MenuBarViewModel()
        {
            MenuItems = Seed();
        }

        public ObservableCollection<IMenuItemViewModel> MenuItems { get; protected set; }

        #region Command methods

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
                MenuItemViewModel.LoadInstance(Resources.MenuItems.FileEN, null,
                    new List<IMenuItemViewModel>
                    {
                        MenuItemViewModel.LoadInstance("Option 1", new RelayCommand(Execute)),
                        MenuItemViewModel.LoadInstance("Option 2", new RelayCommand(Execute))
                    }),
                MenuItemViewModel.LoadInstance(Resources.MenuItems.EditEN, null,
                    new List<IMenuItemViewModel>
                    {
                        MenuItemViewModel.LoadInstance("Option 1", new RelayCommand(Execute)),
                        MenuItemViewModel.LoadInstance("Option 2", new RelayCommand(Execute))
                    }),
                MenuItemViewModel.LoadInstance(Resources.MenuItems.ViewEN, null,
                    new List<IMenuItemViewModel>
                    {
                        MenuItemViewModel.LoadInstance("Option 1", new RelayCommand(Execute)),
                        MenuItemViewModel.LoadInstance("Option 2", new RelayCommand(Execute))
                    }),
                MenuItemViewModel.LoadInstance(Resources.MenuItems.AboutEN, null,
                    new List<IMenuItemViewModel>
                    {
                        MenuItemViewModel.LoadInstance("Option 1", new RelayCommand(Execute)),
                        MenuItemViewModel.LoadInstance("Option 2", new RelayCommand(Execute))
                    })
            };
            return menuItems;
        }

        #endregion
    }
}