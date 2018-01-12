using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel
{
	public interface IMainWindowViewModel : IViewModel
	{
		string Header { get; set; }
	}

	public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
	{
		private string _header;

		public MainWindowViewModel()
		{
			_header = "Test header";
		}

		public string Header
		{
			get => _header;
			set => SetProperty(ref _header, value);
		}
	}
}