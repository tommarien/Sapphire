namespace Sapphire.Commanding
{
    public class DirectExecutingCommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory _factory;

        public DirectExecutingCommandDispatcher(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }

        public void Dispatch<T>(T command) where T : ICommand
        {
            IHandle<T> handler = _factory.CreateHandler<T>();

            try
            {
                handler.Handle(command);
            }
            finally
            {
                _factory.Release(handler);
            }
        }
    }
}