namespace Sapphire.EventDispatchers
{
    public class NullEventDispatcher : IEventDispatcher
    {
        public void Dispatch<T>(T @event) where T : IEvent
        {
        }
    }
}