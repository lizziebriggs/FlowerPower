using UnityEngine;

namespace Flowers
{
    [CreateAssetMenu(fileName = "New Flower", menuName = "Flower/New Flower")]
    
    [System.Serializable]
    public class Flower : ScriptableObject
    {
        public int startingPollen;
        public int pollenIncrease;
        public Sprite flowerSprite;
    }
}
