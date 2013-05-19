using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sapphire.Commanding;

namespace Sapphire.Boot.Installers
{
    public class CommandingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICommandHandlerFactory>().AsFactory());

            container.Register(Component.For<ICommandDispatcher>()
                                        .ImplementedBy<DirectExecutingCommandDispatcher>()
                                        .LifestyleTransient());

            container.Register(Component.For<IBus>()
                                        .ImplementedBy<DispatchingCommandBus>()
                                        .LifestyleTransient());
        }
    }
}