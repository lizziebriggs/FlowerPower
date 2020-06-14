using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Managers
{
    public class MatchThreeManager : MonoBehaviour
    {
        public static MatchThreeManager Instance;

        [SerializeField] private int threeTileMatch = 50, fourTileMatch = 75, fiveTileMatch = 100;
        [SerializeField] private int maxMovesCount;

        public int ThreeTileMatch => threeTileMatch;
        public int FourTileMatch => fourTileMatch;
        public int FiveTileMatch => fiveTileMatch;
        public int MaxMovesCount => maxMovesCount;

        private void Start()
        {
            Instance = GetComponent<MatchThreeManager>();
            DontDestroyOnLoad(this);
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
