namespace Sapphire.Commands
{
    public interface IBus
    {
        void Send(ICommand command);
        void Send(ICommand[] commands);
    }
}