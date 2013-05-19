namespace Sapphire
{
    public interface ISubscribe<T>
    {
        void On(T @event);
    }
}