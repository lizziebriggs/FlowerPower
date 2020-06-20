using UnityEngine;

namespace Managers
{
    public class SfxManager : MonoBehaviour
    {
        public static SfxManager Instance;
        
        [Header("Match Three SFX")]
        public AudioSource tilesMatch;

        private void Start()
        {
            Instance = GetComponent<SfxManager>();
        }

        private void Awake()
        {
            // Make sure manager is singleton
            if (!Instance)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }
    }
}
