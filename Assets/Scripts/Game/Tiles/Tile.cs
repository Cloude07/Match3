using UnityEngine;

namespace Game.Tiles
{
    [RequireComponent (typeof (SpriteRenderer))]
    internal class Tile : MonoBehaviour
    {
        public TileConfig TileConfi { get; private set; }
        public bool IsInteractable { get; private set; }
        public bool IsMatched { get; private set; }

        public void SetTileConfig(TileConfig tileConfig)
        {
            TileConfi = tileConfig;
            IsInteractable = TileConfi.IsInteractable;
            IsMatched = false;

            GetComponent<SpriteRenderer>().sprite = tileConfig.Sprite;
        }

        public bool SetMatch(bool value) => IsMatched = value;
        
    }
}
