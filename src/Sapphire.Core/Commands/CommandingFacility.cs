using Castle.Core.Configuration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Sapphire.Commands
{
    public class CommandingFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.EnsureDependentFacilityAdded<TypedFactoryFacility>();

            kernel.Register(Component.For<ICommandHandlerFactory>().AsFactory());

            kernel.Register(Component.For<ICommandDispatcher>()
                                     .ImplementedBy<DirectExecutingCommandDispatcher>()
                                     .LifestyleTransient());

            kernel.Register(Component.For<IBus>()
                                     .ImplementedBy<DispatchingCommandBus>()
                                     .LifestyleTransient());
        }

        public void Terminate()
        {
        }
    }
}