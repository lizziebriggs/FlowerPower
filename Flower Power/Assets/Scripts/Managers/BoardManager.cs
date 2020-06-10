using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class BoardManager : MonoBehaviour
    {
        public static BoardManager instance;
        public List<Sprite> tileCharacters = new List<Sprite>();
        public GameObject tile;
        public int xSize, ySize;

        private GameObject[,] _tiles; // 2D array
        
        public bool IsShifting { get; set; }
        
        
        private void Start()
        {
            instance = GetComponent<BoardManager>();

            Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
            CreateBoard(offset.x, offset.y);
        }


        private void CreateBoard(float xOffset, float yOffset)
        {
            _tiles = new GameObject[xSize, ySize];

            Vector2 start = transform.position;

            Sprite[] spritesLeft = new Sprite[ySize];
            Sprite spriteBelow = null;

            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    GameObject newTile = Instantiate(tile, new Vector3(start.x + (xOffset * x), start.y + (yOffset * y), 0), tile.transform.rotation);
                    _tiles[x, y] = newTile;

                    newTile.transform.parent = transform;
                    
                    List<Sprite> possibleCharacters = new List<Sprite>();
                    possibleCharacters.AddRange(tileCharacters);

                    possibleCharacters.Remove(spritesLeft[y]);
                    possibleCharacters.Remove(spriteBelow);
                    
                    Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
                    newTile.GetComponent<SpriteRenderer>().sprite = newSprite;

                    spritesLeft[y] = newSprite;
                    spriteBelow = newSprite;
                }
            }
        }
    }
}
