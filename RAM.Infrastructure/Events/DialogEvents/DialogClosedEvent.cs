using Prism.Events;
using RAM.Domain.Helpers;

namespace RAM.Infrastructure.Events.DialogEvents
{
    public class DialogClosedEvent : PubSubEvent<DialogResult>
    {
    }
}