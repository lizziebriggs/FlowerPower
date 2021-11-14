using Flowers;
using IdleGame;
using UnityEngine;

namespace Managers
{
    public class FlowerShopManager : MonoBehaviour
    {
        public static FlowerShopManager Instance;

        [SerializeField] private  FlowerPatch[] _flowerPatches;
        public FlowerPatch[] FlowerPatches => _flowerPatches;
        
        private FlowerController _requestedFlowerToBuy;
        public FlowerController RequestedFlowerToBuy => _requestedFlowerToBuy;

        private void Start()
        {
            Instance = GetComponent<FlowerShopManager>();
        }

        public void Purchase(FlowerController flowerToBuy)
        {
            if (!flowerToBuy)
                return;

            _requestedFlowerToBuy = flowerToBuy;

            foreach (FlowerPatch flowerPatch in _flowerPatches)
            {
                flowerPatch.CurrentState = FlowerPatch.State.Purchasing;
            }
        }
    }
}
