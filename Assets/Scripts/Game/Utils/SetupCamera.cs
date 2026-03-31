using UnityEngine;

namespace Game.Utils
{
    public class SetupCamera : ISetupCamera
    {
        private bool _isVertical;

        public void SetCamera(int width, int height, bool isVertical)
        {
            _isVertical = isVertical;
            float xPos = width / 2f - 0.5f;
            float yPos = height / 2f + 0.5f;
            Camera.main.gameObject.transform.position = new Vector3(xPos, yPos, -10f);
            Camera.main.orthographicSize = GetOrthoSize(width, height);

        }

        private float GetOrthoSize(int width, int height)
        {
            return _isVertical ? (width + 15f) * Screen.height / Screen.width * 0.5f
                : (height + 5f) * Screen.height / Screen.width;
        }
    }
}
