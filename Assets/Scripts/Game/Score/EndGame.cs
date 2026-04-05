using Audio;
using Data;
using save;
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
        private SaveProgress _saveProgress;

        public GameData GameData { get { return _gameData; } }

        public EndGame(GameData gameData, AudioManager audioManager, IAsyncSceneLoading asyncSceneLoading, SaveProgress saveProgress)
        {
            _gameData = gameData;
            _audioManager = audioManager;
            _asyncSceneLoading = asyncSceneLoading;
            _saveProgress = saveProgress;
        }

        public async void End(bool isSuccess)
        {
            if (isSuccess && _gameData.CurrentLevel.LevelNumber
                == _gameData.CurrentLevelIndex)
                _gameData.OpenNextLevel();
            _saveProgress.SaveData();
            _audioManager.StopMusic();
            await _asyncSceneLoading.UnloadAsync(Scenes.GAME);
            await _asyncSceneLoading.LoadAsync(Scenes.MENU);
            _audioManager.PlayMenuMusic();
        }

    }
}
