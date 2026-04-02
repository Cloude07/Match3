using Menu.Levels;
using Menu.UI;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _asyngSceneLoading;
        private SetupLevelSeguence _setupLevel;
        private LevelSeguenceView _seguenceView;
        private MenuView _menuView;

        public MenuEntryPoint(IAsyncSceneLoading asyngSceneLoading, 
            SetupLevelSeguence levelSeguence, LevelSeguenceView seguenceView, MenuView menuView)
        {
            _asyngSceneLoading = asyngSceneLoading;
            _setupLevel = levelSeguence;
            _seguenceView = seguenceView;
            _menuView = menuView;
        }

        public async void Initialize()
        {
            await _setupLevel.Setup(3);
            // music menu
            _seguenceView.SetupButtonsView(3);
            _asyngSceneLoading.LoadingIsDone(true);
            await _menuView.StartAnumation();
            // button enebled
        }
    }
}
