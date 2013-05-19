using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sapphire.Eventing;
using Sapphire.Eventing.Dispatchers;

namespace Sapphire.Boot.Installers
{
    public class EventingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISubscriberFactory>()
                                        .AsFactory());

            container.Register(Component.For<IEventDispatcher>()
                                        .ImplementedBy<DispatchToSubscribersEventDispatcher>());
        }
    }
}