namespace Sapphire.Eventing
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(T @event) where T : IEvent;
    }
}