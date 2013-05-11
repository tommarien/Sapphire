using System;
using Moq;
using NUnit.Framework;
using Sapphire.Commands;

namespace Sapphire.Tests.Commands
{
    [TestFixture]
    public class DirectExecutingCommandDispatcherTests
    {
        [SetUp]
        public void Setup()
        {
            _factory = new Mock<ICommandHandlerFactory>();
            _dispatcher = new DirectExecutingCommandDispatcher(_factory.Object);
        }

        private Mock<ICommandHandlerFactory> _factory;
        private DirectExecutingCommandDispatcher _dispatcher;


        [Test]
        public void Dispatch_Should_get_an_instance_from_the_factory()
        {
            _factory
                .Setup(f => f.CreateHandler<AnyCommand>())
                .Returns(new AnyCommandHandler()).Verifiable("The commandhandler was not requested from the factory");

            _dispatcher.Dispatch(new AnyCommand());

            _factory.Verify();
        }

        [Test]
        public void Dispatch_Should_invoke_the_commandhandler()
        {
            var commandHandler = new AnyCommandHandler();

            _factory
                .Setup(f => f.CreateHandler<AnyCommand>())
                .Returns(commandHandler);

            _dispatcher.Dispatch(new AnyCommand());

            Assert.IsTrue(commandHandler.Handled);
        }

        [Test]
        public void Dispatch_Should_release_the_commandhandler()
        {
            var commandHandler = new AnyCommandHandler();

            _factory
                .Setup(f => f.CreateHandler<AnyCommand>())
                .Returns(commandHandler);

            _factory.Setup(f => f.Release(commandHandler)).Verifiable("The commandhandler should have been released.");

            _dispatcher.Dispatch(new AnyCommand());

            _factory.Verify();
        }

        [Test]
        public void Dispatch_Should_release_the_commandhandler_no_matter_what()
        {
            var commandhandler = Mock.Of<ICommandHandler<AnyCommand>>();

            Mock.Get(commandhandler)
                .Setup(h => h.Handle(It.IsAny<AnyCommand>()))
                .Throws<InvalidOperationException>();

            _factory
                .Setup(f => f.CreateHandler<AnyCommand>())
                .Returns(commandhandler);

            _factory.Setup(f => f.Release(commandhandler))
                    .Verifiable("The commandhandler should have been released not matter what.");

            Assert.Catch(() => _dispatcher.Dispatch(new AnyCommand()));

            _factory.Verify();
        }
    }
}