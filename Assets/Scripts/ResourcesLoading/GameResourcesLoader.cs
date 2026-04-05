using Cysharp.Threading.Tasks;
using Data;
using Game.Tiles;
using Levels;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ResourcesLoading
{
    public class GameResourcesLoader : IDisposable
    {
        public GameObject TilePrefab { get; private set; }
        public GameObject BackgroundTilePrefab { get; private set; }
        public GameObject FxPrefab { get; private set; }
        public TileConfig BlankConfig { get; private set; }
        public Sprite DarkTile { get; private set; }
        public Sprite LightTile { get; private set; }
        public List<TileConfig> CurrentTileSet { get; private set; }
        private GameData _gameData;
        private CancellationTokenSource _cts;

        public GameResourcesLoader(GameData gameData) => _gameData = gameData;

        public async UniTask Load()
        {
            CurrentTileSet = new List<TileConfig>();
            if (_gameData.CurrentLevel.TileSets == TileSets.Kingdom)
                await LoadSet("Kingdom");

            if (_gameData.CurrentLevel.TileSets == TileSets.Gem)
                await LoadSet("Gem");

            await LoadTilesPrefabs();
            await LoadBlankTile();
            await LoadBlanckgroundSprites();

        }

        private async UniTask LoadSet(string key)
        {
            _cts = new CancellationTokenSource();
            var set = Addressables.LoadAssetAsync<TileSetConfig>(key);
            await set.ToUniTask();
            if (set.Status == AsyncOperationStatus.Succeeded)
            {
                CurrentTileSet = set.Result.Set;
                Addressables.Release(set);
            }
            _cts.Cancel();

        }

        private async UniTask LoadTilesPrefabs()
        {
            _cts = new CancellationTokenSource();
            var tile = Addressables.LoadAssetAsync<GameObject>("TilePrefab");

            _cts = new CancellationTokenSource();
            var backgroundTile = Addressables.LoadAssetAsync<GameObject>("BackgroundTilePrefab");

            _cts = new CancellationTokenSource();
            var FxTile = Addressables.LoadAssetAsync<GameObject>("FXPrefab");

            await tile.ToUniTask();
            await backgroundTile.ToUniTask();
            await FxTile.ToUniTask();

            if (tile.Status == AsyncOperationStatus.Succeeded
           && backgroundTile.Status == AsyncOperationStatus.Succeeded
           && FxTile.Status == AsyncOperationStatus.Succeeded)
            {
                TilePrefab = tile.Result;
                BackgroundTilePrefab = backgroundTile.Result;
                FxPrefab = FxTile.Result;

                Addressables.Release(tile);
                Addressables.Release(backgroundTile);
                Addressables.Release(FxTile);
            }
            _cts.Cancel();
        }

        private async UniTask LoadBlankTile()
        {
            _cts = new CancellationTokenSource();
            var blank = Addressables.LoadAssetAsync<TileConfig>("BlankTile");
            await blank.ToUniTask();
            if (blank.Status == AsyncOperationStatus.Succeeded)
            {
                BlankConfig = blank.Result;
                Addressables.Release(blank);
            }
                _cts.Cancel();

        }

        private async UniTask LoadBlanckgroundSprites()
        {
            _cts = new CancellationTokenSource();
            var darkSprite = Addressables.LoadAssetAsync<Sprite>("DarkBG");
            var lightSprite = Addressables.LoadAssetAsync<Sprite>("LightBG");
            await darkSprite.ToUniTask();
            await lightSprite.ToUniTask();
            if (darkSprite.Status == AsyncOperationStatus.Succeeded &&
                lightSprite.Status == AsyncOperationStatus.Succeeded)
            {
                DarkTile = darkSprite.Result;
                LightTile = lightSprite.Result;
                Addressables.Release(darkSprite);
                Addressables.Release(lightSprite);
            }
            _cts.Cancel();

        }

        public void Dispose()
        {
            _cts.Dispose();
        }
    }
}
