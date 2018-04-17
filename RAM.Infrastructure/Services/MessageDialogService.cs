using System.Windows;
using RAM.Domain.Helpers;

namespace RAM.Infrastructure.Services
{
    public interface IMessageDialogService : IService
    {
        //DialogResult ShowYesNoDialog(string title, string message);
    }

    public class MessageDialogService : IMessageDialogService
    {
        //public DialogResult ShowYesNoDialog(string title, string message)
        //{
        //    return new YesNoDialog(title, message)
        //    {
        //        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        //        Owner = App.Current.MainWindow
        //    }
        //    .ShowDialog().GetValueOrDefault()
        //    ? DialogResult.Yes
        //    : DialogResult.No;
        //}
    }
}
