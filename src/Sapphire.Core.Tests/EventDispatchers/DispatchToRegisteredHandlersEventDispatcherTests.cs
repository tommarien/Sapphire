using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Sapphire.EventDispatchers;

namespace Sapphire.Tests.EventDispatchers
{
    [TestFixture]
    public class DispatchToRegisteredHandlersEventDispatcherTests
    {
        [SetUp]
        public void Setup()
        {
            _eventHandlerFactory = Mock.Of<IEventHandlerFactory>();
            _dispatcher = new DispatchToRegisteredHandlersEventDispatcher(_eventHandlerFactory);

            _registeredEventHandlers = new List<IEventHandler<AnyEvent>>
                {
                    Mock.Of<IEventHandler<AnyEvent>>(),
                    Mock.Of<IEventHandler<AnyEvent>>()
                };

            Mock.Get(_eventHandlerFactory)
                .Setup(f => f.GetHandlers<AnyEvent>())
                .Returns(_registeredEventHandlers.ToArray);
        }

        private IEventHandlerFactory _eventHandlerFactory;
        private DispatchToRegisteredHandlersEventDispatcher _dispatcher;
        private List<IEventHandler<AnyEvent>> _registeredEventHandlers;

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

            Mock.Get(_eventHandlerFactory).Verify(x => x.GetHandlers<AnyEvent>());
        }

        [Test]
        public void It_Should_release_all_used_handlers()
        {
            _dispatcher.Dispatch(new AnyEvent());

            Mock.Get(_eventHandlerFactory).Verify(f => f.Release(_registeredEventHandlers[0]));
            Mock.Get(_eventHandlerFactory).Verify(f => f.Release(_registeredEventHandlers[1]));
        }

        [Test]
        public void It_Should_release_all_used_handlers_even_if_one_of_them_fails()
        {
            Mock.Get(_registeredEventHandlers[0])
                .Setup(h => h.On(It.IsAny<AnyEvent>()))
                .Throws<InvalidOperationException>();

            Assert.Throws<InvalidOperationException>(() => _dispatcher.Dispatch(new AnyEvent()));

            Mock.Get(_eventHandlerFactory).Verify(f => f.Release(_registeredEventHandlers[0]));
            Mock.Get(_eventHandlerFactory).Verify(f => f.Release(_registeredEventHandlers[1]));
        }
    }
}