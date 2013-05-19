using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Sapphire.Eventing;
using Sapphire.Eventing.Dispatchers;

namespace Sapphire.Tests.EventDispatchers
{
    [TestFixture]
    public class DispatchToRegisteredHandlersEventDispatcherTests
    {
        [SetUp]
        public void Setup()
        {
            _subscriberFactory = Mock.Of<ISubscriberFactory>();
            _dispatcher = new DispatchToSubscribersEventDispatcher(_subscriberFactory);

            _registeredEventHandlers = new List<ISubscribe<AnyEvent>>
                {
                    Mock.Of<ISubscribe<AnyEvent>>(),
                    Mock.Of<ISubscribe<AnyEvent>>()
                };

            Mock.Get(_subscriberFactory)
                .Setup(f => f.GetSubscribers<AnyEvent>())
                .Returns(_registeredEventHandlers.ToArray);
        }

        private ISubscriberFactory _subscriberFactory;
        private DispatchToSubscribersEventDispatcher _dispatcher;
        private List<ISubscribe<AnyEvent>> _registeredEventHandlers;

        [Test]
        public void It_Should_execute_all_registered_handlers()
        {
            var anEvent = new AnyEvent();

            _dispatcher.Dispatch(anEvent);

            Mock.Get(_registeredEventHandlers[0]).Verify(h => h.On(anEvent));
            Mock.Get(_registeredEventHandlers[1]).Verify(h => h.On(anEvent));
        }

        [Test]
        public void It_Should_get_all_handlers_from_the_factory()
        {
            _dispatcher.Dispatch(new AnyEvent());

            Mock.Get(_subscriberFactory).Verify(x => x.GetSubscribers<AnyEvent>());
        }

        [Test]
        public void It_Should_release_all_used_handlers()
        {
            _dispatcher.Dispatch(new AnyEvent());

            Mock.Get(_subscriberFactory).Verify(f => f.Release(_registeredEventHandlers[0]));
            Mock.Get(_subscriberFactory).Verify(f => f.Release(_registeredEventHandlers[1]));
        }

        [Test]
        public void It_Should_release_all_used_handlers_even_if_one_of_them_fails()
        {
            Mock.Get(_registeredEventHandlers[0])
                .Setup(h => h.On(It.IsAny<AnyEvent>()))
                .Throws<InvalidOperationException>();

            Assert.Throws<InvalidOperationException>(() => _dispatcher.Dispatch(new AnyEvent()));

            Mock.Get(_subscriberFactory).Verify(f => f.Release(_registeredEventHandlers[0]));
            Mock.Get(_subscriberFactory).Verify(f => f.Release(_registeredEventHandlers[1]));
        }
    }
}