using Flowers;
using UnityEngine;

namespace IdleGame
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        [SerializeField] private GameObject _backButton;
        
        [Header("Camera Setting")]
        [SerializeField] private Camera _camera;
        [SerializeField] private float _defaultDepth = -10; 
        [SerializeField] private float _defaultZoom = 5;
        [SerializeField] private float _zoomAmount = 2.5f;
        
        private Vector3 starPos = new Vector3(0, 0, -10);

        private FlowerPatch _focusedFlowerPatch;

        private void Start()
        {
            Instance = GetComponent<CameraController>();
        }

        private void Awake()
        {
            ResetPosition();
        }

        public void ZoomTo(FlowerPatch flowerPatchToFocus)
        {
            _focusedFlowerPatch = flowerPatchToFocus;
            var patchPosition = _focusedFlowerPatch.transform.position;
            
            _camera.transform.position = new Vector3(patchPosition.x, patchPosition.y, _defaultDepth);
            _camera.orthographicSize = _zoomAmount;
            
            _backButton.SetActive(true);
        }

        public void ResetPosition()
        {
            if(_focusedFlowerPatch)
                _focusedFlowerPatch.CurrentState = FlowerPatch.FlowerPatchState.Idle;
            
            _camera.transform.position = starPos;
            _camera.orthographicSize = _defaultZoom;
            
            _backButton.SetActive(false);
        }
    }
}
