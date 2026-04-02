using Levels;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSequenceConfigs", menuName = "Configs/LevelSequenceConfigs")]
public class LevelSequenceConfig : ScriptableObject
{
    [SerializeField] private List<LevelConfig> _levelSequence = new List<LevelConfig>();
    public List<LevelConfig> LevelSequence => _levelSequence;

    private void OnValidate()
    {
        if(_levelSequence.Count != 5)
            throw new ArgumentOutOfRangeException(
                "Level sequence must contain 5 elements");

    }
}
