using Moq;
using NUnit.Framework;
using Sapphire.Eventing;
using Sapphire.Eventing.Dispatchers;

namespace Sapphire.Tests
{
    [TestFixture]
    public class DomainEventsTests
    {
        [SetUp]
        public void Setup()
        {
            _eventDispatcher = new Mock<IEventDispatcher>();
            DomainEvents.Dispatcher = _eventDispatcher.Object;
        }

        private Mock<IEventDispatcher> _eventDispatcher;

        [Test]
        public void Raise_Should_dispatch_to_dispatcher()
        {
            var anyEvent = new AnyEvent();

            _eventDispatcher.Setup(d => d.Dispatch(anyEvent)).Verifiable("Raise an event should delegate to dispatcher");

            DomainEvents.Raise(anyEvent);

            _eventDispatcher.Verify();
        }

        [Test]
        public void Setting_Dispatcher_to_null_should_set_dispatcher_to_null_dispatcher()
        {
            DomainEvents.Dispatcher = null;

            Assert.IsInstanceOf<NullEventDispatcher>(DomainEvents.Dispatcher);
        }
    }
}