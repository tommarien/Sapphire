using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Moq;
using NUnit.Framework;
using Sapphire.Eventing;

namespace Sapphire.Tests
{
    [TestFixture]
    [Category("Integration")]
    public class EventingTests
    {
        [SetUp]
        public void Setup()
        {
            _windsorContainer = new WindsorContainer();
            Boot.Boot.Strap(_windsorContainer);

            _anyEventHandler = Mock.Of<ISubscribe<AnyEvent>>();

            _windsorContainer.Register(Component.For<ISubscribe<AnyEvent>>().Instance(_anyEventHandler));

            IEventDispatcher dispatcher = _windsorContainer.Resolve<IEventDispatcher>();

            DomainEvents.Dispatcher = dispatcher;
        }

        [TearDown]
        public void TearDown()
        {
            _windsorContainer.Dispose();
        }

        private ISubscribe<AnyEvent> _anyEventHandler;
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