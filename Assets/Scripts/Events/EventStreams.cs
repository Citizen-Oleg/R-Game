using SimpleEventBus;
using SimpleEventBus.Interfaces;

namespace Events
{
    public static class EventStreams
    {
        public static IEventBus UserInterface { get; } = new EventBus();
    }
}
