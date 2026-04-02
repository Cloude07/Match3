using Menu;
using Menu.Levels;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class MenuScop : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntryPoint>();
            builder.Register<SetupLevelSeguence>(Lifetime.Singleton);
        }
    }
}
