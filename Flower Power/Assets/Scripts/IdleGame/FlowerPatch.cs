using Flowers;
using Managers;
using UnityEngine;

namespace IdleGame
{
    public class FlowerPatch : MonoBehaviour
    {
        public enum FlowerPatchState {Idle, Purchasing, LevellingUp};
        
        private FlowerPatchState _currentState;
        public FlowerPatchState CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        [SerializeField] private Transform[] _flowerPositions;
        [SerializeField] private FlowerController[] _flowersInPatch = new FlowerController[4];

        private int _numberOfFlowersInPatch;
        private bool _hasAvailableSpace = true;

        private void Awake()
        {
            _currentState = FlowerPatchState.Idle;
        }

        private void OnMouseDown()
        {
            switch (_currentState)
            {
                case FlowerPatchState.Idle:
                    _currentState = FlowerPatchState.LevellingUp;
                    CameraController.Instance.ZoomTo(this);
                    break;
                
                case FlowerPatchState.Purchasing:
                    PlantFlower(FlowerShopManager.Instance.RequestedFlowerToBuy);
                    break;
                
                case FlowerPatchState.LevellingUp:
                    break;
                
                default:
                    break;
            }
        }

        private void PlantFlower(FlowerController flowerToPlant)
        {
            if (!_hasAvailableSpace)
                return;

            for (int i = 0; i < _flowersInPatch.Length; i++)
            {
                if (_flowersInPatch[i]) continue;
                
                _flowersInPatch[i] = Instantiate(flowerToPlant, _flowerPositions[i].position, Quaternion.Euler(0, 0, 0));
                _numberOfFlowersInPatch++;

                IdleManager.Instance.TotalPollen -= flowerToPlant.FlowerType.startingPollen;
                
                break;
            }

            if (_numberOfFlowersInPatch == _flowersInPatch.Length) _hasAvailableSpace = false; 
                
            foreach (FlowerPatch flowerPatch in FlowerShopManager.Instance.FlowerPatches)
            {
                flowerPatch.CurrentState = FlowerPatch.FlowerPatchState.Idle;
            }
        }
    }
}
