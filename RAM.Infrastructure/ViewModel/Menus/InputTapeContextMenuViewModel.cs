using Prism.Events;
using RAM.Infrastructure.ViewModel.Base;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel.Menus
{
    public interface IInputTapeContextMenuViewModel : IContextMenuViewModel
    {
    }

    public class InputInputTapeContextMenuViewModelBase : ContextMenuViewModelBase<TapeMemberWrapper>,
        IInputTapeContextMenuViewModel
    {
        public InputInputTapeContextMenuViewModelBase(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}