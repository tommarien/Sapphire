namespace Sapphire.Eventing.Dispatchers
{
    public class DispatchToSubscribersEventDispatcher : IEventDispatcher
    {
        private readonly ISubscriberFactory _subscriberFactory;

        public DispatchToSubscribersEventDispatcher(ISubscriberFactory subscriberFactory)
        {
            _subscriberFactory = subscriberFactory;
        }

        public void Dispatch<T>(T @event) where T : IEvent
        {
            ISubscribe<T>[] subscribers = _subscriberFactory.GetSubscribers<T>();

            try
            {
                foreach (var subscriber in subscribers)
                    subscriber.On(@event);
            }
            finally
            {
                foreach (var handler in subscribers)
                    _subscriberFactory.Release(handler);
            }
        }
    }
}