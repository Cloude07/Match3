using Menu.Levels;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _asyngSceneLoading;
        private SetupLevelSeguence _levelSeguence;

        public MenuEntryPoint(IAsyncSceneLoading asyngSceneLoading, 
            SetupLevelSeguence levelSeguence)
        {
            _asyngSceneLoading = asyngSceneLoading;
            _levelSeguence = levelSeguence;
        }

        public async void Initialize()
        {
            await _levelSeguence.Setup(1);
            // music menu
            _asyngSceneLoading.LoadingIsDone(true);
            // await animating
            // button enebled
        }
    }
}
