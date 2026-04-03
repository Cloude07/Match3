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
        [SerializeField] private MenuView _menuView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntryPoint>();
            builder.Register<SetupLevelSeguence>(Lifetime.Singleton);
            builder.Register<StartGame>(Lifetime.Singleton);
            builder.RegisterInstance(_seguenceView);
            builder.RegisterInstance(_menuView);
        }
    }
}
