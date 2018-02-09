using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Events;
using RAM.Domain.Model;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.Startup;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
    public interface IMenuItemViewModel : IViewModel
    {
        MenuItemWrapper Model { get; set; }
        ICommand Command { get; }
        ObservableCollection<IMenuItemViewModel> Children { get; set; }

        void Load(MenuItemWrapper menuItemWrapper, ICommand command,
            IEnumerable<IMenuItemViewModel> children);
    }

    public class MenuItemViewModel : BaseViewModel, IMenuItemViewModel
    {
        private static Func<string> _getHeader;

        private ObservableCollection<IMenuItemViewModel> _childMenuItems;
        private MenuItemWrapper _model;

        public MenuItemViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LoadLocalizationStrings);
            _childMenuItems = new ObservableCollection<IMenuItemViewModel>();
        }

        public ICommand Command { get; protected set; }

        public ObservableCollection<IMenuItemViewModel> Children
        {
            get => _childMenuItems;
            set => SetProperty(ref _childMenuItems, value);
        }

        public MenuItemWrapper Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public void Load(MenuItemWrapper menuItemWrapper, ICommand command,
            IEnumerable<IMenuItemViewModel> children)
        {
            Model = menuItemWrapper;
            Command = command;
            Children = new ObservableCollection<IMenuItemViewModel>(children);
        }

        // This method exists becouse of author's lazyness 
        public static IMenuItemViewModel LoadInstance(Func<string> getHeader,
            ICommand command, IEnumerable<IMenuItemViewModel> children = null)
        {
            _getHeader = getHeader;
            var menuItem = new MenuItemViewModel(Container.Resolve<IEventAggregator>());

            menuItem.Load(new MenuItemWrapper(new MenuItem(_getHeader())), command,
                children ?? new List<IMenuItemViewModel>());

            return menuItem;
        }

        #region Event handlers

        private void LoadLocalizationStrings()
        {
            Model.Header = _getHeader();
        }

        #endregion
    }
}