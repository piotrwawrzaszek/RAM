namespace RAM.Infrastructure.ViewModel.Base
{
    public interface IDialogViewModel : IViewModel
    {
        string DialogTitle { get; }
        string OkButtonText { get; }
        string CancelButtonText { get; }
        string DeleteButtonText { get; }
    }
}