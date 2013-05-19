namespace Sapphire.Tests.Commands
{
    public class AnyCommandHandler : IHandle<AnyCommand>
    {
        public bool Handled { get; private set; }

        public void Handle(AnyCommand command)
        {
            Handled = true;
        }
    }
}