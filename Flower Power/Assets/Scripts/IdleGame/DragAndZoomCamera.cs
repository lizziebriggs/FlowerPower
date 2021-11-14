using UnityEngine;

namespace IdleGame
{
    [RequireComponent(typeof(Camera))]
    public class DragAndZoomCamera : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;

        [Header("Settings")]
        [SerializeField] private bool _zoomEnabled;
        [SerializeField] private float _zoomSpeed = 0.01f;
        [SerializeField] private float _zoomOutMin = 1;
        [SerializeField] private float _zoomOutMax = 8;

        private Vector3 _touchStart;

        private void Start()
        {
            if (!_mainCamera) _mainCamera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _touchStart = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
            // Mobile input camera zoom
            if (Input.touchCount == 2)
            {
                if (!_zoomEnabled) return;
            
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
        
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
        
                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
        
                float difference = currentMagnitude - prevMagnitude;
            
                Zoom(difference * _zoomSpeed);
            }
        
            // Camera pan
            else if (Input.GetMouseButton(0))
            {
                Vector3 direction = _touchStart - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _mainCamera.transform.position += direction;
            }
        
            // Mouse input camera zoom
            else if(_zoomEnabled)
                Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        private void Zoom(float zoomAmount)
        {
            _mainCamera.orthographicSize = Mathf.Clamp(_mainCamera.orthographicSize - zoomAmount, _zoomOutMin, _zoomOutMax);
        }
    }
}
