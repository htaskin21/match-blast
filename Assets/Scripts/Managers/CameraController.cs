using UnityEngine;

namespace Managers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private float _borderSize;

        /// <summary>
        /// Sets up the camera position and orthographic size based on the board size.
        /// </summary>
        public void Setup(int row, int column, Vector2 blockSize)
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            var boardWidth = column * blockSize.x;
            var boardHeight = row * blockSize.y;

            SetCameraPosition(boardHeight, boardWidth, blockSize);
            SetOrthographicSize(boardHeight, boardWidth);
        }

        private void SetCameraPosition(float boardHeight, float boardWidth, Vector2 blockSize)
        {
            var halfBlockOffset = new Vector3(blockSize.x / 2f, blockSize.y / 2f, 0f);
            var boardOrigin = Vector3.zero;
            var boardCenter = boardOrigin + new Vector3(boardWidth / 2f, boardHeight / 2f, 0f) - halfBlockOffset;

            _camera!.transform.position = new Vector3(boardCenter.x, boardCenter.y, -10f);
        }

        private void SetOrthographicSize(float boardHeight, float boardWidth)
        {
            var aspectRatio = Screen.width / (float)Screen.height;
            var verticalSize = boardHeight / 2f + _borderSize;
            var horizontalSize = (boardWidth / 2f + _borderSize) / aspectRatio;

            _camera!.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
        }
    }
}