
using TMPro;
using UnityEngine;

namespace Game.GridSystem
{
    public class GameDebug
    {
        private IGrid _grid;

        public GameDebug(IGrid grid)
        {
            _grid = grid;
        }

        public void ShowwDebug(Transform parant)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    var text = x + "," + y;
                    CreateDebugText(parant, text,
                        _grid.GridToWorld(x,y));
                }
            }
        }

        private void CreateDebugText(Transform parant, string text, Vector3 position)
        {
            var debugText = new GameObject("DebugText"
                ,typeof(TextMeshPro));
            debugText.transform.SetParent(parant);
            debugText.transform.position = position + new Vector3(0,0,-1);
            debugText.transform.forward = Vector3.forward;

            var TMP = debugText.GetComponent<TextMeshPro>();
            TMP.text = text;
            TMP.fontSize = 3f;
            TMP.color = Color.white;
            TMP.alignment = TextAlignmentOptions.Center;
        }
    }
}
