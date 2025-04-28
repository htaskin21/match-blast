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
        public void Setup(int row, int column)
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            _camera!.transform.position = new Vector3((float)(column - 1) / 2, (float)(row - 1) / 2, -10);

            var aspectRatio = (float)Screen.width / (float)Screen.height;
            var vertical = (float)row / 2 + _borderSize;
            var horizontal = ((float)column / 2 + _borderSize) / aspectRatio;
            _camera.orthographicSize = (horizontal > vertical) ? horizontal : vertical;
        }
    }
}