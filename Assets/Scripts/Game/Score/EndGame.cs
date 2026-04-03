using Audio;
using Data;
using SceneLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Score
{
    public class EndGame
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _asyncSceneLoading;

        public GameData GameData { get { return _gameData; } }

        public EndGame(GameData gameData, AudioManager audioManager, IAsyncSceneLoading asyncSceneLoading)
        {
            _gameData = gameData;
            _audioManager = audioManager;
            _asyncSceneLoading = asyncSceneLoading;
        }

        public async void End(bool isSuccess)
        {
            if (isSuccess && _gameData.CurrentLevel.LevelNumber
                == _gameData.CurrentLevelIndex)
                _gameData.OpenNextLevel();
            _audioManager.StopMusic();
            await _asyncSceneLoading.UnloadAsync(Scenes.GAME);
            await _asyncSceneLoading.LoadAsync(Scenes.MENU);
            _audioManager.PlayMenuMusic();
        }

    }
}
