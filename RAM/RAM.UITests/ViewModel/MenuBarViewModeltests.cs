using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FluentAssertions;
using Moq;
using RAM.Infrastructure.ViewModel;
using RAM.Infrastructure.ViewModel.Wrapper;
using Xunit;

namespace RAM.UITests.ViewModel
{
	public class MenuBarViewModeltests
	{
		public MenuBarViewModeltests()
		{
			_menuBarViewModel = new MenuBarViewModel();
		}
        
		private readonly IMenuBarViewModel _menuBarViewModel;
	}
}