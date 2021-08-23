using Flowers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PurchaseButtonController : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Flower _flower;

        void Update()
        {
            CheckPollenBalance();
        }

        private void CheckPollenBalance()
        { 
            if(!IdleManager.Instance)
                return;

            if (IdleManager.Instance.TotalPollen < _flower.startingPollen)
                _button.interactable = false;
            else if (IdleManager.Instance.TotalPollen >= _flower.startingPollen)
                _button.interactable = true;
        }
    }
}
