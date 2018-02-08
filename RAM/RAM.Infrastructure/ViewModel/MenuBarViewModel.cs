using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.Resources.MenuItems;
using static RAM.Infrastructure.ViewModel.MenuItemViewModel;

namespace RAM.Infrastructure.ViewModel
{
    public interface IMenuBarViewModel : IViewModel
    {
        ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; }
    }

    public class MenuBarViewModel : BaseViewModel, IMenuBarViewModel
    {
        public MenuBarViewModel()
        {
            MenuItemViewModels = Seed();
        }

        public ObservableCollection<IMenuItemViewModel> MenuItemViewModels { get; protected set; }

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
                LoadInstance(MenuItems.File, null,
                    new List<IMenuItemViewModel>
                    {
                        LoadInstance("Option 1", new RelayCommand(Execute)),
                        LoadInstance("Option 2", new RelayCommand(Execute))
                    }),
                LoadInstance(MenuItems.Edit, null,
                    new List<IMenuItemViewModel>
                    {
                        LoadInstance("Option 1", new RelayCommand(Execute)),
                        LoadInstance("Option 2", new RelayCommand(Execute))
                    }),
                LoadInstance(MenuItems.View, null,
                    new List<IMenuItemViewModel>
                    {
                        LoadInstance("Option 1", new RelayCommand(Execute)),
                        LoadInstance("Option 2", new RelayCommand(Execute))
                    }),
                LoadInstance(MenuItems.About, null,
                    new List<IMenuItemViewModel>
                    {
                        LoadInstance("Option 1", new RelayCommand(Execute)),
                        LoadInstance("Option 2", new RelayCommand(Execute))
                    })
            };
            return menuItems;
        }

        #endregion
    }
}