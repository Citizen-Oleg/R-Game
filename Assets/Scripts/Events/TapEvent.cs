using SimpleEventBus.Events;

namespace Events
{
    public class TapEvent : EventBase
    {
        public float Power { get; }

        public TapEvent(float power)
        {
            Power = power;
        }
    }
}