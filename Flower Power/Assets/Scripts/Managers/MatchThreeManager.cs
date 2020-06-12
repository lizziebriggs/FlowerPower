using UnityEngine;

namespace Managers
{
    public class MatchThreeManager : MonoBehaviour
    {
        public static MatchThreeManager Instance;

        public int maxMovesCount;
        
        private void Start()
        {
            Instance = GetComponent<MatchThreeManager>();
            
            if (!Instance)
            {
                DontDestroyOnLoad(gameObject);
                Instance = GetComponent<MatchThreeManager>();
            }
            else Destroy(gameObject);
        }
    }
}
