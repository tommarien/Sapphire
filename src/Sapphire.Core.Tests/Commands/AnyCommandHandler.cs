using Sapphire.Commands;

namespace Sapphire.Tests.Commands
{
    public class AnyCommandHandler : ICommandHandler<AnyCommand>
    {
        public bool Handled { get; private set; }

        public void Handle(AnyCommand command)
        {
            Handled = true;
        }
    }
}