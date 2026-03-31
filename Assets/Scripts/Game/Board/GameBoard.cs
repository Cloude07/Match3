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
<<<<<<< HEAD
        [SerializeField]
        private GameObject _tilePrefab;

=======
>>>>>>> ObjectPool
        [Header("Test")]
        [SerializeField]
        private TileConfig _tileConfig;
        [SerializeField]
        private bool _isVertical = false;
        [SerializeField]
        private Vector2 _sizeBoard;

<<<<<<< HEAD
        private readonly List<Tile> _tilesToRefill = new List<Tile>();

        private IGrid _grid;
        private ISetupCamera _setupCamera;
=======
        private TilePool _tilePool;

        private readonly List<Tile> _tilesToRefill = new List<Tile>();
        private IGrid _grid;
        private SetupCamera _setupCamera;
>>>>>>> ObjectPool

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
<<<<<<< HEAD
                    var tile = Instantiate(_tilePrefab, transform);
                    tile.transform.position = _grid.GridToWorld(x, y);
                   var tileComponent = tile.GetComponent<Tile>();
                    tileComponent.SetTileConfig(_tileConfig);
                    _grid.SetValue(x, y, tileComponent);
=======
                    var tile = _tilePool.GetTile(_grid.GridToWorld(x, y),transform);
                    _grid.SetValue(x, y, tile);
                    tile.gameObject.SetActive(true);
                    _tilesToRefill.Add(tile);
>>>>>>> ObjectPool
                }
            }
        }

        [Inject]
<<<<<<< HEAD
        private void Construct(IGrid grid, ISetupCamera setupCamera)
        {
            _grid = grid;
            _setupCamera = setupCamera;
=======
        private void Construct(IGrid grid, SetupCamera setupCamera, TilePool tilePool)
        {
            _grid = grid;
            _setupCamera = setupCamera;
            _tilePool = tilePool;
>>>>>>> ObjectPool
        }


    }
}
