using Prism.Events;

namespace RAM.Infrastructure.Events.MenuItemEvents
{
    public class DeleteEvent<T> : PubSubEvent<T>
    {
    }
}