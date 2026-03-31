using Game.Tiles;
using UnityEngine;

namespace Game.GridSystem
{
    public interface IGrid
    {
        Vector2Int CurrentPosition { get; }
        Tile[,] GameGrid { get; }
        int Height { get; }
        Vector2Int TargetPosition { get; }
        int Width { get; }

        Tile GetValue(int x, int y);
        Tile GetValue(Vector3 worldPosition);
        Vector3 GridToWorld(int x, int y);
        bool IsValidPosition(int x, int y);
        Vector2Int SetCurrentPosition(Vector2Int value);
        Vector2Int SetTargerPosition(Vector2Int value);
        void SetupGrid(int width, int height);
        void SetValue(int x, int y, Tile tile);
        void SetValue(Vector3 worldPosition, Tile tile);
        Vector2Int WorldToGrid(Vector3 worldPosition);
    }
}