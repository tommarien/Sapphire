using Sapphire.EventDispatchers;

namespace Sapphire
{
    public static class DomainEvents
    {
        private static volatile IEventDispatcher _dispatcher = new NullEventDispatcher();
        private static readonly object Syncroot = new object();

        public static IEventDispatcher Dispatcher
        {
            get { return _dispatcher; }
            set
            {
                lock (Syncroot)
                    _dispatcher = value ?? new NullEventDispatcher();
            }
        }

        public static void Raise<T>(T @event) where T : IEvent
        {
            Dispatcher.Dispatch(@event);
        }
    }
}