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
        [SerializeField] private Text finalScoreText;
        
        [Header("Game Over HUD")]
        [SerializeField] private GuiBase gameOverPanel;
        [SerializeField] private GameObject newHighScoreText;
        [SerializeField] private Text highScoreText;

        private int _score, _movesCount;
        
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                scoreText.text = "Score:\n" + _score;
            }
        }
        
        public int MovesCount
        {
            get => _movesCount;
            set
            {
                _movesCount = value;
                movesCountText.text = "Moves Left:\n" + _movesCount;
            }
        }


        private void Start()
        {
            Instance = GetComponent<GuiManager>();
            
            gameOverPanel.Hide();
            newHighScoreText.SetActive(false);
            CheckHighScore();
            
            Score = 0;
            MovesCount = MatchThreeManager.Instance.MaxMovesCount;
        }

        private void Update()
        {
            if (_movesCount != 0) return;
            
            CheckHighScore();
            
            finalScoreText.text = "Final score: " + _score;
            highScoreText.text = "High score: " + MatchThreeManager.Instance.HighScore;
            gameOverPanel.Show();
        }

        private void CheckHighScore()
        {
            if (_score <= MatchThreeManager.Instance.HighScore) return;
            
            MatchThreeManager.Instance.HighScore = _score;
            MatchThreeManager.Instance.SaveHighScore();
            newHighScoreText.SetActive(true);
        }
    }
}
