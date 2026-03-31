using Game.GridSystem;
using Game.Tiles;
using Game.Utils;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField]
        private TileConfig _tileConfig;
        [SerializeField]
        private bool _isVertical = false;
        [SerializeField]
        private Vector2 _sizeBoard;


        private readonly List<Tile> _tilesToRefill = new List<Tile>();

        private IGrid _grid;
        private ISetupCamera _setupCamera;

        private TilePool _tilePool;


        private void Start()
        {
            _grid.SetupGrid((int)_sizeBoard.x, (int)_sizeBoard.y);
            CreateBoard();
            _setupCamera.SetCamera(_grid.Width, _grid.Height, _isVertical);
        }

        public void CreateBoard()
        {
            FillBoard();
        }

        private void FillBoard()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GetValue(x, y)) continue;

                    var tile = _tilePool.GetTile(_grid.GridToWorld(x, y),transform);
                    _grid.SetValue(x, y, tile);
                    tile.gameObject.SetActive(true);
                    _tilesToRefill.Add(tile);

                }
            }
        }

        [Inject]
        private void Construct(IGrid grid, ISetupCamera setupCamera, TilePool tilePool)
        {
            _grid = grid;
            _setupCamera = setupCamera;
            _tilePool = tilePool;
        }


    }
}
