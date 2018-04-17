using RAM.Infrastructure.Resources.Controls;

namespace RAM.Infrastructure.ViewModel.Base
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        #region Properties

        private string _okButtonText;
        private string _cancelButtonText;
        private string _deleteButtonText;

        public string OkButtonText
        {
            get => _okButtonText;
            protected set => SetProperty(ref _okButtonText, value);
        }

        public string CancelButtonText
        {
            get => _cancelButtonText;
            protected set => SetProperty(ref _cancelButtonText, value);
        }

        public string DeleteButtonText
        {
            get => _deleteButtonText;
            protected set => SetProperty(ref _deleteButtonText, value);
        }

        #endregion

        #region Event handlers

        protected override void LoadLocalizationStrings()
        {
            OkButtonText = Controls.OkButtonText;
            CancelButtonText = Controls.CancelButtonText;
            DeleteButtonText = Controls.DeleteButtonText;
        }

        #endregion


    }
}