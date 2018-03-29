using Prism.Events;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.ViewModel.Menus
{
    public interface IStatementContextMenuViewModel : IContextMenuViewModel
    {
    }

    public class StatementContextMenuViewModel : ContextMenuViewModel<StatementWrapper>, 
        IStatementContextMenuViewModel
    {
        public StatementContextMenuViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}