using System.Linq;
using Castle.Core.Configuration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

namespace Sapphire.Commands
{
    public class CommandingFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            AssertFacility<TypedFactoryFacility>(kernel);

            kernel.Register(Component.For<ICommandHandlerFactory>().AsFactory());

            kernel.Register(Component.For<ICommandDispatcher>()
                                     .ImplementedBy<DirectExecutingCommandDispatcher>()
                                     .LifestyleTransient());

            kernel.Register(Component.For<IBus>()
                                     .ImplementedBy<DispatchingCommandBus>()
                                     .LifestyleSingleton());
        }

        public void Terminate()
        {
        }

        private void AssertFacility<T>(IKernel kernel)
        {
            if (kernel.GetFacilities().Any(f => f is T)) return;
            throw new FacilityException(string.Format("CommandingFacility is dependent on {0}", typeof (T).Name));
        }
    }
}