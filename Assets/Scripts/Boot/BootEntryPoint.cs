using DG.Tweening;
using save;
using SceneLoading;
using UnityEngine;
using VContainer.Unity;

namespace Boot
{
    public class BootEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SaveProgress _saveProgress;

        public BootEntryPoint(IAsyncSceneLoading sceneLoading, SaveProgress saveProgress)
        {
            _sceneLoading = sceneLoading;
            _saveProgress = saveProgress;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;   //Фиксируем FPS на 60
            Screen.sleepTimeout = SleepTimeout.NeverSleep; // если для телефона, чтобы пока игра активна экран не гас
            DOTween.SetTweensCapacity(5000, 100); //ограничение анимаций DOTWEEN
            _saveProgress.LoadData();
            await _sceneLoading.LoadAsync(Scenes.MENU);
        }
    }
}