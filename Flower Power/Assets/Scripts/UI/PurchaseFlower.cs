using Flowers;
using UnityEngine;

namespace UI
{
    public class PurchaseFlower : MonoBehaviour
    {
        public void Purchase(FlowerController flowerToBuy)
        {
            if (!flowerToBuy)
                return;
            
            Instantiate(flowerToBuy, Vector3.zero, Quaternion.Euler(0, 0, 0));
            Debug.Log(flowerToBuy.name + " bought!");
        }
    }
}
