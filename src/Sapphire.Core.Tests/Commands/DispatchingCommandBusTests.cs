using Moq;
using NUnit.Framework;
using Sapphire.Commanding;

namespace Sapphire.Tests.Commands
{
    [TestFixture]
    public class DispatchingCommandBusTests
    {
        [SetUp]
        public void Setup()
        {
            _dispatcher = new Mock<ICommandDispatcher>(MockBehavior.Strict);
            _commandBus = new Mock<DispatchingCommandBus>(_dispatcher.Object).Object;
        }

        private Mock<ICommandDispatcher> _dispatcher;

        private DispatchingCommandBus _commandBus;

        [Test]
        public void SendShouldDispatchCommandWithAppropriateType()
        {
            _commandBus = new DispatchingCommandBus(_dispatcher.Object);
            var anyCommand = new AnyCommand();

            _dispatcher.Setup(d => d.Dispatch(anyCommand));

            _commandBus.Send(anyCommand);
        }

        [Test]
        public void SendShouldIgnoreNullCommands()
        {
            _commandBus = new DispatchingCommandBus(_dispatcher.Object);

            _commandBus.Send((ICommand) null);
        }

        [Test]
        public void SendWithArrayOfCommandsShouldInvokeSendForEachOfTheArray()
        {
            var firstCommand = Mock.Of<ICommand>();
            var secondCommand = Mock.Of<ICommand>();

            Mock.Get(_commandBus).Setup(b => b.Send(firstCommand)).Verifiable();
            Mock.Get(_commandBus).Setup(b => b.Send(secondCommand)).Verifiable();

            _commandBus.Send(new[] {firstCommand, secondCommand});

            Mock.Get(_commandBus).Verify();
        }

        [Test]
        public void SendWithArrayOfCommandsShouldNotSendAnyThingIfArrayIsEmpty()
        {
            Mock.Get(_commandBus).Setup(b => b.Send(It.IsAny<ICommand>()));

            _commandBus.Send(new ICommand[0]);

            Mock.Get(_commandBus).Verify(x => x.Send(It.IsAny<ICommand>()), Times.Never());
        }

        [Test]
        public void SendWithArrayOfCommandsShouldNotSendAnyThingIfArrayIsNull()
        {
            Mock.Get(_commandBus).Setup(b => b.Send(It.IsAny<ICommand>()));

            _commandBus.Send((ICommand[]) null);

            Mock.Get(_commandBus).Verify(x => x.Send(It.IsAny<ICommand>()), Times.Never());
        }
    }
}