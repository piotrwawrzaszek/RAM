using System.Drawing;
using System.Windows;
using System.Windows.Input;
using Prism.Events;
using RAM.Domain.Helpers;
using RAM.Infrastructure.Command;
using RAM.Infrastructure.Events.DialogEvents;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel.Dialogs
{
    public interface IYesNoDialogViewModel : IDialogViewModel
    {
        string Message { get; set; }
        new string DialogTitle { get; set; }
        Image Symbol { get; set; }

        ICommand YesCommand { get; }
        ICommand NoCommand { get; }

        void Load(string title, string message, Image symbol);
    }

    public class YesNoDialogViewModel : DialogViewModelBase, IYesNoDialogViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public YesNoDialogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            LoadLocalizationStrings();
        }

        public void Load(string title, string message, Image symbol)
        {
            DialogTitle = title;
            Message = message;
            Symbol = symbol;
        }

        protected sealed override void LoadLocalizationStrings()
        {
            base.LoadLocalizationStrings();
        }

        #region Properties

        private string _dialogTitle;
        private string _message;
        private Image _symbol;

        public string DialogTitle
        {
            get => _dialogTitle;
            set => SetProperty(ref _dialogTitle, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public Image Symbol
        {
            get => _symbol;
            set => SetProperty(ref _symbol, value);
        }

        public ICommand YesCommand => new RelayCommand(OnYesClicked);
        public ICommand NoCommand => new RelayCommand(OnNoClicked);

        #endregion

        #region Command methods
        
        private void OnYesClicked(object sender)
        {
            if(!(sender is Window window)) return;
            _eventAggregator.GetEvent<DialogClosedEvent>().Publish(DialogResult.Yes);
            window.Close();
        }

        private void OnNoClicked(object sender)
        {
            if (!(sender is Window window)) return;
            _eventAggregator.GetEvent<DialogClosedEvent>().Publish(DialogResult.No);
            window.Close();
        }

        #endregion
    }
}
