using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel
{
    public interface IRegisterPanelViewModel : IViewModel
    {
    }

    public class RegisterPanelViewModel : BaseViewModel, IRegisterPanelViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public RegisterPanelViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
