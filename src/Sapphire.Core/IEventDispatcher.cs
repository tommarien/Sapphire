namespace Sapphire
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(T @event) where T : IEvent;
    }
}