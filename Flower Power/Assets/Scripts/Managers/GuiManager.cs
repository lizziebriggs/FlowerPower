using GUI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GuiManager : MonoBehaviour
    {
        public static GuiManager Instance;
        
        [Header("HUD")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Text movesCountText;
        [SerializeField] private GuiBase gameOverPanel;
        [SerializeField] private Text finalScoreText;

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
        
        public int MovesCount
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
            Instance = GetComponent<GuiManager>();
            gameOverPanel.Hide();
            Score = 0;
            MovesCount = MatchThreeManager.Instance.MaxMovesCount;
        }

        private void Update()
        {
            if (_movesCount != 0) return;
            
            finalScoreText.text = "Final score: " + _score.ToString();
            gameOverPanel.Show();
        }
    }
}
