using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class PurchaseButtonController : MonoBehaviour
    {
        private Button _button;
        void Start()
        {
            _button = GetComponent<Button>();
        }
        
        void Update()
        {
            CheckPollenBalance();
        }
        
        private void CheckPollenBalance()
        { 
            if(!IdleManager.Instance)
                return;
            
            // does player have enough monies?
        }
    }
}
