using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GUIManager : MonoBehaviour
    {
        public static GUIManager Instance;
        
        [Header("HUD")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Text movesCountText;

        private int _score, _movesCount;
        
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                scoreText.text = "Score:\n" + _score.ToString();
            }
        }
        
        public int MovesCounter
        {
            get => _movesCount;
            set
            {
                _movesCount = value;
                movesCountText.text = "Moves Left:\n" + _movesCount.ToString();
            }
        }


        private void Start()
        {
            Instance = GetComponent<GUIManager>();

            _movesCount = MatchThreeManager.Instance.maxMovesCount;
        }
    }
}
