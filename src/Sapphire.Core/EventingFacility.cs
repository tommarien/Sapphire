using Castle.Core.Configuration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Sapphire.EventDispatchers;

namespace Sapphire
{
    public class EventingFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.EnsureDependentFacilityAdded<TypedFactoryFacility>();

            kernel.Register(Component.For<IEventHandlerFactory>()
                                     .AsFactory());

            kernel.Register(Component.For<IEventDispatcher>()
                                     .ImplementedBy<DispatchToRegisteredHandlersEventDispatcher>());

            DomainEvents.Dispatcher = kernel.Resolve<IEventDispatcher>();
        }

        public void Terminate()
        {
            DomainEvents.Dispatcher = null;
        }
    }
}