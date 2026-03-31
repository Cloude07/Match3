using ResourcesLoading;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Tiles
{
    public class TilePool
    {

        private List<Tile> _tilePool = new List<Tile>();
        private IObjectResolver _objectResolver;
        private GameResourcesLoader _gameResourcesLoader;

        public TilePool(IObjectResolver objectResolver, GameResourcesLoader gameResourcesLoader)
        {
            _objectResolver = objectResolver;
            _gameResourcesLoader = gameResourcesLoader;
        }

        public Tile GetTile(Vector3 position, Transform parant)
        {
            for (int i = 0; i < _tilePool.Count; i++)
            {
                if (_tilePool[i].gameObject.activeInHierarchy) continue;
                _tilePool[i].SetTileConfig(GetRandomTileConfig());
                _tilePool[i].gameObject.transform.position = position;
                return _tilePool[i];
            }
            var tile = CreateTile(position, parant);
            return tile;
        }

        public Tile CreateBlankTile(Vector3 position, Transform parant)
        {
            var blankPrefab = _objectResolver.Instantiate(_gameResourcesLoader.BlankPrefab, 
                position, Quaternion.identity, parant);

            var blamkTile = blankPrefab.GetComponent<Tile>();
            blamkTile.SetTileConfig(_gameResourcesLoader.BlankConfig);
            return blamkTile;
        }

        private Tile CreateTile(Vector3 position, Transform parant)
        {
            var tilePrefab = _objectResolver.Instantiate(_gameResourcesLoader.TilePrefab, 
                position, Quaternion.identity, parant);

            var tile = tilePrefab.GetComponent<Tile>();
            tile.SetTileConfig(GetRandomTileConfig());
            _tilePool.Add(tile);
            return tile;
        }

        private TileConfig GetRandomTileConfig() => _gameResourcesLoader.TileSetConfig
            .Set[Random.Range(0, _gameResourcesLoader.TileSetConfig.Set.Count)];
    }
}
