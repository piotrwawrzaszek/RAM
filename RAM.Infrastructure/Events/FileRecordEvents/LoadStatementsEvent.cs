using Prism.Events;
using RAM.Infrastructure.ViewModel.Wrapper;

namespace RAM.Infrastructure.Events.FileRecordEvents
{
    public class LoadStatementsEvent : PubSubEvent<FileRecordWrapper>
    {
    }
}