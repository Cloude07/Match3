using Animation;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Tiles;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        private readonly List<Tile> _tilesToRefill = new List<Tile>();
        private BlankTilesSetup _blankTilesSetup;
        private IGrid _grid;

        private TilePool _tilePool;
        private IAnimation _animation;
        private MatchFinder _matchFinder;

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
        private void Construct(IGrid grid, TilePool tilePool,
            IAnimation animation, MatchFinder matchFinder, BlankTilesSetup blankTilesSetup)
        {
            _grid = grid;
            _tilePool = tilePool;
            _animation = animation;
            _matchFinder = matchFinder;
            _blankTilesSetup = blankTilesSetup;
        }


    }
}
