using Flowers;
using UnityEngine;

namespace Managers
{
    public class IdleManager : MonoBehaviour
    {
        public static IdleManager Instance;
        
        [SerializeField] private int totalPollen;

        public int TotalPollen
        {
            get => totalPollen;
            set
            { 
                totalPollen = value;
                MainGuiManager.Instance.PollenText.text = "Pollen: " + totalPollen;
            }
        }

        void Start()
        {
            Instance = GetComponent<IdleManager>();
        }


        public void LevelUpFlower(FlowerController flower)
        {
            flower.Level += 1;
        }
    }
}
