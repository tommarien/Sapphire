using System.Reflection;

namespace Sapphire.Commanding
{
    public class DispatchingCommandBus : IBus
    {
        private readonly ICommandDispatcher _dispatcher;

        public DispatchingCommandBus(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public virtual void Send(ICommand command)
        {
            if (command == null) return;

            MethodInfo method = typeof (ICommandDispatcher).GetMethod("Dispatch");
            MethodInfo generic = method.MakeGenericMethod(command.GetType());
            generic.Invoke(_dispatcher, new object[] {command});
        }

        public void Send(ICommand[] commands)
        {
            if (commands == null) return;

            foreach (ICommand command in commands) Send(command);
        }
    }
}