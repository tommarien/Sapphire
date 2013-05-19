namespace Sapphire
{
    public interface IHandle<T>
    {
        void Handle(T message);
    }
}