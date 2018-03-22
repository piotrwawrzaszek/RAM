sing System.Collections.Generic;
using System.Collections.ObjectModel;
using RAM.Infrastructure.Resources;
using RAM.Infrastructure.Wrapper;

namespace RAM.Infrastructure.Data
{
    public class MenuItemProvider : IMenuItemProvider
    {
        private readonly List<MenuItem> _menuItems;

        public MenuItemProvider()
        {
            _menuItems = new List<MenuItem>
            {
                new MenuItem(null) {Header = Resource.FileEN},
                new MenuItem(null) {Header = Resource.ViewEN},
                new MenuItem(null) {Header = Resource.EditEN},
                new MenuItem(null) {Header = Resource.HelpEN}
            };

            _menuItems[0].ChildMenuItems =
                new ObservableCollection<MenuItem>
                {
                    new MenuItem(_menuItems[0]) {Header = "Option 1"},
                    new MenuItem(_menuItems[0]) {Header = "Option 2"},
                    new MenuItem(_menuItems[0]) {Header = "Option 3"},
                    new MenuItem(_menuItems[0]) {Header = "Option 4"},
                    new MenuItem(_menuItems[0]) {Header = "Option 5"}
                };
        }

        public IEnumerable<MenuItem> GetAllMenuItems()
        {
            return _menuItems;
        }
    }
}