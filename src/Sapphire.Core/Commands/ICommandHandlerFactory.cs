namespace Sapphire.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> CreateHandler<T>() where T : ICommand;
        void Release(object handler);
    }
}