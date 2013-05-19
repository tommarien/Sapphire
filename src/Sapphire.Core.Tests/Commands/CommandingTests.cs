using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Sapphire.Tests.Commands
{
    [TestFixture]
    [Category("Integration")]
    public class CommandingTests
    {
        [SetUp]
        public void Setup()
        {
            _windsorContainer = new WindsorContainer();
            Boot.Boot.Strap(_windsorContainer);

            _anyCommandHandler = new AnyCommandHandler();

            _windsorContainer.Register(Component.For<IHandle<AnyCommand>>().Instance(_anyCommandHandler));
        }

        [TearDown]
        public void TearDown()
        {
            _windsorContainer.Dispose();
        }

        private AnyCommandHandler _anyCommandHandler;
        private IWindsorContainer _windsorContainer;

        [Test]
        public void SendCommandUntilHandled()
        {
            var bus = _windsorContainer.Resolve<IBus>();

            bus.Send(new AnyCommand());

            Assert.IsTrue(_anyCommandHandler.Handled);
        }
    }
}