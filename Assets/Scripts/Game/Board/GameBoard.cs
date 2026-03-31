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
        [SerializeField]
        private GameObject _tilePrefab;

        [Header("Test")]
        [SerializeField]
        private TileConfig _tileConfig;
        [SerializeField]
        private bool _isVertical = false;
        [SerializeField]
        private Vector2 _sizeBoard;

        private readonly List<Tile> _tilesToRefill = new List<Tile>();
        private IGrid _grid;
        private SetupCamera _setupCamera;

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
                    var tile = Instantiate(_tilePrefab, transform);
                    tile.transform.position = _grid.GridToWorld(x, y);
                   var tileComponent = tile.GetComponent<Tile>();
                    tileComponent.SetTileConfig(_tileConfig);
                    _grid.SetValue(x, y, tileComponent);
                }
            }
        }

        [Inject]
        private void Construct(IGrid grid, SetupCamera setupCamera)
        {
            _grid = grid;
            _setupCamera = setupCamera;
        }


    }
}
