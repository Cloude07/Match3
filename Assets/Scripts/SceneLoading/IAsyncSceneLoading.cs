using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAsyncSceneLoading
{
    UniTask LoadAsync(string sceneName);
    UniTask UnloadAsync(string sceneName);
    void LoadingIsDone(bool isValue);
}
