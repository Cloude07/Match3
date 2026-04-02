using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace SceneLoading
{
    public class AsyncSceneLoading : IAsyncSceneLoading
    {
       private readonly Dictionary<string, SceneInstance> _loadedScence = 
            new Dictionary<string, SceneInstance>();

        private LoadingView _loadingView;
        private CancellationTokenSource _cts;

        public async UniTask LoadAsync(string sceneName)
        {
            _cts = new CancellationTokenSource();
            LoadingIsDone(false);

            //Так как проект маленький загрузка не отображается, была добавлена задержка для илюзии загрузки
            await UniTask.Delay(TimeSpan.FromSeconds(2f), _cts.IsCancellationRequested);

            var loadedScene = await Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive)
                .WithCancellation(_cts.Token);
            SceneManager.SetActiveScene(loadedScene.Scene);
            if(_loadedScence.ContainsKey(sceneName) == false)
                _loadedScence.Add(sceneName, loadedScene);
            _cts.Cancel();
        }

        public void LoadingIsDone(bool value)
        {
            _loadingView.SetActiveScreen(value != true);
        }

        public async UniTask UnloadAsync(string sceneName)
        {
            _cts = new CancellationTokenSource();
            var sceneInstance = _loadedScence[sceneName];
            await Addressables.UnloadSceneAsync(sceneInstance)
                .WithCancellation(_cts.Token).AsUniTask();
            _loadedScence.Remove(sceneName);
            _cts.Cancel();
        }

        [Inject]
        private void Construct(LoadingView loadingView)
        {
           _loadingView = loadingView;
        }

    }
}
