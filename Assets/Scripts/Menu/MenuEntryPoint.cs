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

        public MenuEntryPoint(IAsyncSceneLoading asyngSceneLoading, 
            SetupLevelSeguence levelSeguence, LevelSeguenceView seguenceView)
        {
            _asyngSceneLoading = asyngSceneLoading;
            _setupLevel = levelSeguence;
            _seguenceView = seguenceView;
        }

        public async void Initialize()
        {
            await _setupLevel.Setup(3);
            // music menu
            _seguenceView.SetupButtonsView(3);
            _asyngSceneLoading.LoadingIsDone(true);
            // await animating
            // button enebled
        }
    }
}
