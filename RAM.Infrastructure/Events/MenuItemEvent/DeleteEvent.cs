using Prism.Events;

namespace RAM.Infrastructure.Events.MenuItemEvent
{
    public class DeleteEvent<T> : PubSubEvent<T>
    {
    }
}