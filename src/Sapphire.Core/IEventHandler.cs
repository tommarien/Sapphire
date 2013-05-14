namespace Sapphire
{
    public interface IEventHandler<T> where T : IEvent
    {
        void On(T @event);
    }
}