using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Menu.Levels
{
    public class SetupLevelSeguence
    {
        public LevelSequenceConfig CurrentLevelSeguence {  get; private set; }

        public async UniTask Setup(int currentLevel)
        {
            if (currentLevel <= 5)
            {
                await LoadLevels("Levels1-5");
                Debug.Log("Levels 1-5");
            }
            else
            {
                await LoadLevels("Levels6-10");
                Debug.Log("Levels 6-10");
            }
        }

        private async UniTask LoadLevels(string key)
        {
           AsyncOperationHandle<LevelSequenceConfig> levels = 
                Addressables.LoadAssetAsync<LevelSequenceConfig>(key);

            await levels.ToUniTask();
            if(levels.Status == AsyncOperationStatus.Succeeded)
            {
                CurrentLevelSeguence = levels.Result;
                Addressables.Release(levels);
            }
        }
    }
}
