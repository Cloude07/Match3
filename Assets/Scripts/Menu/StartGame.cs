using Audio;
using Data;
using Levels;
using SceneLoading;

namespace Menu
{
    public class StartGame
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _asyncSceneLoading;

        public StartGame(GameData gameData, AudioManager audioManager, IAsyncSceneLoading asyncSceneLoading)
        {
            _gameData = gameData;
            _audioManager = audioManager;
            _asyncSceneLoading = asyncSceneLoading;
        }

        public async void Start(LevelConfig level)
        {
            _gameData.SetCurrendLevel(level);
            _audioManager.StopMusic();
            _audioManager.PlayStopMusic();
            await _asyncSceneLoading.UnloadAsync(Scenes.MENU);
            await _asyncSceneLoading.LoadAsync(Scenes.GAME);
            _audioManager.PlayGameMusic();
        }
    }
}
