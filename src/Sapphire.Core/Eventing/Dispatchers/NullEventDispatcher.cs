namespace Sapphire.Eventing.Dispatchers
{
    public class NullEventDispatcher : IEventDispatcher
    {
        public void Dispatch<T>(T @event) where T : IEvent
        {
        }
    }
}