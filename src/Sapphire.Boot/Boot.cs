using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using Sapphire.Boot.Installers;

namespace Sapphire.Boot
{
    public static class Boot
    {
        public static void Strap(IWindsorContainer container)
        {
            container.AddFacility<TypedFactoryFacility>();

            container.Install(new CommandingInstaller(), new EventingInstaller());
        }
    }
}