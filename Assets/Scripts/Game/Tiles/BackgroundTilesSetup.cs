using Animation;
using Cysharp.Threading.Tasks;
using ResourcesLoading;
using System;
using System.Threading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Tiles
{
    public class BackgroundTilesSetup : IDisposable
    {
        private readonly GameResourcesLoader _resourcesLoader;
        private CancellationTokenSource _cts;
        private IObjectResolver _objectResolver;
        private IAnimation _animation;

        public BackgroundTilesSetup(IObjectResolver objectResolver,
            GameResourcesLoader resourcesLoader,
            IAnimation animation)
        {
            _objectResolver = objectResolver;
            _resourcesLoader = resourcesLoader;
            _animation = animation;
        }
        public void Dispose()
        {
            _cts?.Dispose();
            _objectResolver?.Dispose();
        }

        public async UniTask SetupBackground(Transform parent, bool[,] blanks,
            int width, int height)
        {
            _cts = new CancellationTokenSource();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (blanks[x, y])
                        continue;

                    var bacgroundTile = CreateBacgroundTile(
                        new Vector3(x, y, 0.1f), parent);
                    if (x % 2 == 0 && y % 2 == 0 ||
                        x % 2 != 0 && y % 2 != 0)
                      bacgroundTile.GetComponent<SpriteRenderer>().sprite = _resourcesLoader.DarkTile;
                    else
                        bacgroundTile.GetComponent<SpriteRenderer>().sprite = _resourcesLoader.LightTile;
                    var duration = UnityEngine.Random.Range(0.8f, 1.5f);
                    _animation.Reveal(bacgroundTile, duration);
                }
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), _cts.IsCancellationRequested);
            _cts.Cancel();
        }

        private GameObject CreateBacgroundTile(Vector3 position, Transform parant) =>
            _objectResolver.Instantiate(_resourcesLoader.BackgroundTilePrefab, position,
                Quaternion.identity, parant);


    }
}
