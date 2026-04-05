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
        public void Dispose() =>
            _cts?.Dispose();

        public async UniTask Load()
        {
            _cts = new CancellationTokenSource();
            CurrentTileSet = new List<TileConfig>();
            await LoadSet();
            TilePrefab = await Loader<GameObject>("TilePrefab");
            BackgroundTilePrefab = await Loader<GameObject>("BackgroundTilePrefab");
            FxPrefab = await Loader<GameObject>("FXPrefab");
            BlankConfig = await Loader<TileConfig>("BlankTile");
            DarkTile = await Loader<Sprite>("DarkBG");
            LightTile = await Loader<Sprite>("LightBG");
            _cts.Cancel();
        }

        private async UniTask<T> Loader<T>(string key)
        {
            var assetHandler = Addressables.LoadAssetAsync<T>(key);
            var asset = await assetHandler.ToUniTask();
            return assetHandler.Status == AsyncOperationStatus.Succeeded ? asset : default;
        }

        private async UniTask LoadSet()
        {
            _cts = new CancellationTokenSource();
            switch (_gameData.CurrentLevel.TileSets)
            {
                case TileSets.Kingdom:
                    var tileSets = await Loader<TileSetConfig>("Kingdom");
                    CurrentTileSet = tileSets.Set;
                    break;

                case TileSets.Gem:
                    tileSets = await Loader<TileSetConfig>("Gem");
                    CurrentTileSet = tileSets.Set;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _cts.Cancel();
        }
    }
}
