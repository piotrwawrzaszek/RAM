using Prism.Events;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel.Menus
{
    public interface IInputTapeContextMenuViewModel : IContextMenuViewModel
    {
    }

    public class InputInputTapeContextMenuViewModel : ContextMenuViewModel<TapeMemberWrapper>,
        IInputTapeContextMenuViewModel
    {
        public InputInputTapeContextMenuViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}