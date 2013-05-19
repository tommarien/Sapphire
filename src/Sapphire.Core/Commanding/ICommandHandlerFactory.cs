namespace Sapphire.Commanding
{
    public interface ICommandHandlerFactory
    {
        IHandle<T> CreateHandler<T>() where T : ICommand;
        void Release(object handler);
    }
}