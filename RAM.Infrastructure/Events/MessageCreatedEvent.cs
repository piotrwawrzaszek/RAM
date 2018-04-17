using Prism.Events;

namespace RAM.Infrastructure.Events
{
    public class MessageCreatedEvent : PubSubEvent<string>
    {
    }
}