using Animation;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Tiles;
using Game.Utils;
using Levels;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private TileConfig _tileConfig;
        [SerializeField] private bool _isVertical = false;

        [SerializeField] private bool _isDebug = false;
        [SerializeField] private LevelConfig _levelConfig;

        private readonly List<Tile> _tilesToRefill = new List<Tile>();

        private IGrid _grid;
        private ISetupCamera _setupCamera;
        private BlankTilesSetup _blankTilesSetup;
        private GameDebug _gameDebug;
        private TilePool _tilePool;
        private IAnimation _animation;
        private MatchFinder _matchFinder;

        public LevelConfig LevelConfig => _levelConfig;

        private void Awake()
        {
            _grid.SetupGrid(_levelConfig.Width, _levelConfig.Height);
            _blankTilesSetup.SetupBlanks(_levelConfig);
            _setupCamera.SetCamera(_grid.Width, _grid.Height, _isVertical);
            if (_isDebug)
                _gameDebug.ShowwDebug(transform);
        }
        public void CreateBoard()
        {
            FillBoard();
            while(_matchFinder.CheckBoardForMatches(_grid))
            {
                ClearBoard();
                FillBoard();
                Debug.Log("Created Board");
            }
            _matchFinder.ClearTilesToRemove();
            RevealTiles();
        }

        private void RevealTiles()
        {
            foreach (var tile in _tilesToRefill)
            {
                var gameObjectTile = tile.gameObject;
                _animation.Reveal(gameObjectTile, 1f);
            }
        }

        private void FillBoard()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_blankTilesSetup.Blanks[x, y])
                    {
                        if (_grid.GetValue(x, y)) continue;
                        var blankTile = _tilePool.CreateBlankTile(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x, y, blankTile);
                    }
                    else
                    {
                        var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x, y, tile);
                        tile.gameObject.SetActive(true);
                        _tilesToRefill.Add(tile);
                    }
                }
            }
        }

        private void ClearBoard()
        {
            if (_tilesToRefill == null) return;
            foreach (var tile in _tilesToRefill)
            {
                _grid.SetValue(tile.transform.position, null);
                tile.gameObject.SetActive(false);
            }
            _tilesToRefill.Clear();
        }

        [Inject]
        private void Construct(IGrid grid, ISetupCamera setupCamera,
            TilePool tilePool, GameDebug gameDebug, BlankTilesSetup blankTilesSetup, 
            IAnimation animation, MatchFinder matchFinder)
        {
            _grid = grid;
            _setupCamera = setupCamera;
            _tilePool = tilePool;
            _gameDebug = gameDebug;
            _blankTilesSetup = blankTilesSetup;
            _animation = animation;
            _matchFinder = matchFinder;
        }


    }
}
