using System.Windows;

namespace RAM.UI.Dialogs
{
    public partial class YesNoDialog : Window
    {
        public YesNoDialog(string title, string message)
        {
            InitializeComponent();
            Title = title;
            TextBlock.Text = message;
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}