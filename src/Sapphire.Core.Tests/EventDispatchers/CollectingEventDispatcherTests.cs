using System.Linq;
using NUnit.Framework;
using Sapphire.EventDispatchers;

namespace Sapphire.Tests.EventDispatchers
{
    [TestFixture]
    public class CollectingEventDispatcherTests
    {
        [SetUp]
        public void Setup()
        {
            _collectingEventDispatcher = new CollectingEventDispatcher();
        }

        private CollectingEventDispatcher _collectingEventDispatcher;

        [Test]
        public void It_should_clear_all_collected_events_when_cleared()
        {
            var anyEvent = new AnyEvent();

            _collectingEventDispatcher.Dispatch(anyEvent);

            _collectingEventDispatcher.Clear();

            CollectionAssert.IsEmpty(_collectingEventDispatcher);
        }

        [Test]
        public void It_should_collect_any_event()
        {
            var anyEvent = new AnyEvent();

            _collectingEventDispatcher.Dispatch(anyEvent);

            IEvent[] occurredEvents = _collectingEventDispatcher.ToArray();
            Assert.AreEqual(1, occurredEvents.Count());
            Assert.AreSame(anyEvent, occurredEvents[0]);
        }
    }
}