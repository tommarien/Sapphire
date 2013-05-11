using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Sapphire.Commands;

namespace Sapphire.Tests.Commands
{
    [TestFixture]
    [Category("Integration")]
    public class CommandFacilityTests
    {
        [SetUp]
        public void Setup()
        {
            _windsorContainer = new WindsorContainer();
            _windsorContainer.AddFacility<TypedFactoryFacility>();
            _windsorContainer.AddFacility<CommandingFacility>();

            _anyCommandHandler = new AnyCommandHandler();

            _windsorContainer.Register(Component.For<ICommandHandler<AnyCommand>>().Instance(_anyCommandHandler));
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