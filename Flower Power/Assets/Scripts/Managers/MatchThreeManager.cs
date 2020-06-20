using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Managers
{
    public class MatchThreeManager : MonoBehaviour
    {
        public static MatchThreeManager Instance;

        [SerializeField] private int threeTileMatch = 50, fourTileMatch = 75, fiveTileMatch = 100;
        [SerializeField] private int maxMovesCount;

        public int HighScore { get; set; }

        public int ThreeTileMatch => threeTileMatch;
        public int FourTileMatch => fourTileMatch;
        public int FiveTileMatch => fiveTileMatch;
        public int MaxMovesCount => maxMovesCount;

        private void Start()
        {
            Instance = GetComponent<MatchThreeManager>();
            HighScore = int.Parse(LoadHighScore());
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) ResetHighScore();
        }

        public void SaveHighScore()
        {
            var destination = Application.persistentDataPath + "/high_score.txt";

            FileStream file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);

            string data = GuiManager.Instance.Score.ToString();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        private string LoadHighScore()
        {
            var destination = Application.persistentDataPath + "/high_score.txt";
            FileStream file;
 
            if(File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.LogError("File not found");
                return null;
            }
 
            BinaryFormatter bf = new BinaryFormatter();
            string data = (string) bf.Deserialize(file);
            file.Close();

            return data;
        }

        private void ResetHighScore()
        {
            HighScore = 0;
        }
    }
}
