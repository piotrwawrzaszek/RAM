using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Events;
using RAM.Domain.Model;
using RAM.Infrastructure.Events;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel
{
    public interface IMenuItemViewModel : IViewModel
    {
        MenuItemWrapper Model { get; set; }
        ICommand Command { get; }
        ObservableCollection<IMenuItemViewModel> Children { get; set; }
        IMenuItemViewModel Load(ICommand command, IEnumerable<IMenuItemViewModel> children);
    }

    public class MenuItemViewModel : BaseViewModel, IMenuItemViewModel
    {
        private readonly Func<string> _getHeader;

        private ObservableCollection<IMenuItemViewModel> _childMenuItems;
        private MenuItemWrapper _model;

        public MenuItemViewModel(Func<string> getHeader, IEventAggregator eventAggregator)
        {
            _getHeader = getHeader;

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

        public IMenuItemViewModel Load(ICommand command,
            IEnumerable<IMenuItemViewModel> children = null)
        {
            Model = new MenuItemWrapper(new MenuItem(_getHeader()));
            Command = command;

            if (children != null)
                Children = new ObservableCollection<IMenuItemViewModel>(children);

            return this;
        }

        #region Event handlers

        private void LoadLocalizationStrings()
        {
            Model.Header = _getHeader();
        }

        #endregion
    }
}