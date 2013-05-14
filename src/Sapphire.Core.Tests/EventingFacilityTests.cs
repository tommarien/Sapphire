using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Moq;
using NUnit.Framework;

namespace Sapphire.Tests
{
    [TestFixture]
    [Category("Integration")]
    public class EventingFacilityTests
    {
        [SetUp]
        public void Setup()
        {
            _windsorContainer = new WindsorContainer();
            _windsorContainer.AddFacility<TypedFactoryFacility>();
            _windsorContainer.AddFacility<EventingFacility>();

            _anyEventHandler = Mock.Of<IEventHandler<AnyEvent>>();

            _windsorContainer.Register(Component.For<IEventHandler<AnyEvent>>().Instance(_anyEventHandler));
        }

        [TearDown]
        public void TearDown()
        {
            _windsorContainer.Dispose();
        }

        private IEventHandler<AnyEvent> _anyEventHandler;
        private IWindsorContainer _windsorContainer;

        [Test]
        public void It_Should_be_able_to_handle_without_handlers()
        {
            DomainEvents.Raise(new AnotherEvent());
        }

        [Test]
        public void It_Should_setup_everything_as_expected()
        {
            var anyEvent = new AnyEvent();

            DomainEvents.Raise(anyEvent);

            Mock.Get(_anyEventHandler).Verify(h => h.On(anyEvent));
        }
    }
}