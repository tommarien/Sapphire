namespace Sapphire.EventDispatchers
{
    public class DispatchToRegisteredHandlersEventDispatcher : IEventDispatcher
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;

        public DispatchToRegisteredHandlersEventDispatcher(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Dispatch<T>(T @event) where T : IEvent
        {
            IEventHandler<T>[] handlers = _eventHandlerFactory.GetHandlers<T>();

            try
            {
                foreach (var handler in handlers)
                    handler.On(@event);
            }
            finally
            {
                foreach (var handler in handlers)
                    _eventHandlerFactory.Release(handler);
            }
        }
    }
}