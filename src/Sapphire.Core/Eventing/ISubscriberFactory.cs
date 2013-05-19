namespace Sapphire.Eventing
{
    public interface ISubscriberFactory
    {
        ISubscribe<T>[] GetSubscribers<T>() where T : IEvent;
        void Release(object subscriber);
    }
}