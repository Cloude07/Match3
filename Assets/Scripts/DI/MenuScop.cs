using Menu;
using Menu.Levels;
using Menu.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class MenuScop : LifetimeScope
    {
        [SerializeField] private LevelSeguenceView _seguenceView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntryPoint>();
            builder.RegisterInstance(_seguenceView);
            builder.Register<SetupLevelSeguence>(Lifetime.Singleton);
        }
    }
}
