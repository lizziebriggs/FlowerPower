using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class MainGuiManager : MonoBehaviour
    {
        public static MainGuiManager Instance;
        
        [Header("HUD")]
        [SerializeField] private Text pollenText;

        public Text PollenText
        {
            get => pollenText;
            set => pollenText = value;
        }

        private void Start()
        {
            Instance = GetComponent<MainGuiManager>();
        }
    }
}
