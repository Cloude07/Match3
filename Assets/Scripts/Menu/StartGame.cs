using Audio;
using Data;
using Levels;
using SceneLoading;
using System;
using System.Threading;

namespace Menu
{
    public class StartGame 
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _asyncSceneLoading;
        private CancellationTokenSource _cts;

        public StartGame(GameData gameData, AudioManager audioManager, IAsyncSceneLoading asyncSceneLoading)
        {
            _gameData = gameData;
            _audioManager = audioManager;
            _asyncSceneLoading = asyncSceneLoading;
        }


        public async void Start(LevelConfig level)
        {
            _cts = new CancellationTokenSource();
            _gameData.SetCurrendLevel(level);
            _audioManager.StopMusic();
            _audioManager.PlayStopMusic();
            await _asyncSceneLoading.UnloadAsync(Scenes.MENU);
            await _asyncSceneLoading.LoadAsync(Scenes.GAME);
            _audioManager.PlayGameMusic();
            _cts.Cancel();
        }
    }
}
