using Data;
using UnityEngine;

namespace save
{
    public class SaveProgress
    {
        private const string CURRENT_LEVEL = "Level";
        private const string SOUND = "Sound";
        private GameData _gameData;
        public SaveProgress(GameData gameData) =>
            _gameData = gameData;

        public void SaveData()
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL, _gameData.CurrentLevelIndex);
            PlayerPrefs.SetInt(SOUND, _gameData.IsEnabledSound ? 1 : 0);

        }

        public void LoadData()
        {

            _gameData.SetCurrentLevelIndex(PlayerPrefs.GetInt(CURRENT_LEVEL) >= 1
                ? PlayerPrefs.GetInt(CURRENT_LEVEL) : 1);

            _gameData.SetEnabledSound(PlayerPrefs.GetInt(CURRENT_LEVEL) != 0);
        }

        public void DefuldData()
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL, 1);
            PlayerPrefs.SetInt(SOUND, 1);
        }
    }
}
