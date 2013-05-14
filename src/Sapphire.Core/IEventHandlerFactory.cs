namespace Sapphire
{
    public interface IEventHandlerFactory
    {
        IEventHandler<T>[] GetHandlers<T>() where T : IEvent;
        void Release(object handler);
    }
}