using Audio;
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
        private AudioManager _audioManager;

        public MenuEntryPoint(IAsyncSceneLoading asyngSceneLoading, 
            SetupLevelSeguence levelSeguence, LevelSeguenceView seguenceView, 
            MenuView menuView, AudioManager audioManager)
        {
            _asyngSceneLoading = asyngSceneLoading;
            _setupLevel = levelSeguence;
            _seguenceView = seguenceView;
            _menuView = menuView;
            _audioManager = audioManager;
        }

        public async void Initialize()
        {
            await _setupLevel.Setup(3);
            _audioManager.PlayMenuMusic();
            _seguenceView.SetupButtonsView(3);
            _asyngSceneLoading.LoadingIsDone(true);
            await _menuView.StartAnumation();

        }
    }
}
