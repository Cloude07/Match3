using Game.Tiles;
using UnityEngine;

namespace ResourcesLoading
{
    public class GameResourcesLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private GameObject _blankPrefab;
        [SerializeField] private TileConfig _blankConfig;
        [SerializeField] private TileSetConfig _tileSetConfig;
        [SerializeField] private GameObject _backgroundTilePrefab;
        [SerializeField] private GameObject _fxPrefab;
        [SerializeField] private Sprite _darkTile;
        [SerializeField] private Sprite _lightTile;

        public TileSetConfig TileSetConfig => _tileSetConfig;
        public TileConfig BlankConfig => _blankConfig;
        public GameObject TilePrefab => _tilePrefab;
        public GameObject BlankPrefab => _blankPrefab;
        public GameObject BackgroundTilePrefab => _backgroundTilePrefab;
        public GameObject FxPrefab => _fxPrefab; 
        public Sprite DarkTile => _darkTile; 
        public Sprite LightTile => _lightTile;

    }
}
