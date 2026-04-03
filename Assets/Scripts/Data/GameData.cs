using Levels;
using System;

namespace Data
{
    public class GameData
    {
        public LevelConfig CurrentLevel {  get; private set; }
        public int CurrentLevelIndex { get; private set; }
        public bool IsEnabledSound { get; private set; }

        public GameData()
        {
            IsEnabledSound = true;
            CurrentLevelIndex = 1;
        }

        public void SetCurrentLevelIndex(int index)
        {
            if(index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            CurrentLevelIndex = index;
        }

        public void OpenNextLevel() =>
            CurrentLevelIndex++;

        public bool SetEnabledSound(bool value) => 
            IsEnabledSound = value;

        public void SetCurrendLevel(LevelConfig levelConfig)
        {
            if(levelConfig != null)
                CurrentLevel = levelConfig;
        }

    }
}
