using System;
using System.Collections;
using System.Collections.Generic;
using MatchThree;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class BoardManager : MonoBehaviour
    {
        public static BoardManager Instance;
        
        [Header("Board Values")]
        [SerializeField] private List<Sprite> tileCharacters = new List<Sprite>();
        [SerializeField] private GameObject tile;
        [SerializeField] private int xSize = 8, ySize = 12;
        public float shiftDelay = .03f;

        private GameObject[,] _tiles; // 2D array

        public GameObject[,] Tiles => _tiles;

        public bool IsShifting { get; private set; }
        

        private void Start()
        {
            Instance = GetComponent<BoardManager>();

            Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
            CreateBoard(offset.x, offset.y);
        }


        private void CreateBoard(float xOffset, float yOffset)
        {
            _tiles = new GameObject[xSize, ySize];

            Vector2 start = transform.position;

            Sprite[] spritesLeft = new Sprite[ySize];
            Sprite spriteBelow = null;

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    GameObject newTile = Instantiate(tile, new Vector3(start.x + (xOffset * x), start.y + (yOffset * y), 0), tile.transform.rotation);
                    _tiles[x, y] = newTile;

                    newTile.transform.parent = transform;
                    
                    List<Sprite> possibleCharacters = new List<Sprite>();
                    possibleCharacters.AddRange(tileCharacters);

                    // Avoid adjacent tiles being the same
                    possibleCharacters.Remove(spritesLeft[y]);
                    possibleCharacters.Remove(spriteBelow);
                    
                    Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
                    newTile.GetComponent<SpriteRenderer>().sprite = newSprite;

                    spritesLeft[y] = newSprite;
                    spriteBelow = newSprite;
                }
            }
        }


        public IEnumerator FindNullTiles()
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (_tiles[x, y].GetComponent<SpriteRenderer>().sprite) continue;
                    
                    yield return StartCoroutine(routine: ShiftTilesDown(x, y));
                    break;
                }
            }
            
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++)
                {
                    _tiles[x, y].GetComponent<Tile>().ClearAllMatches();
                }
            }

        }


        private IEnumerator ShiftTilesDown(int x, int yStart)
        {
            IsShifting = true;
            List<SpriteRenderer> renders = new List<SpriteRenderer>();
            int nullCount = 0;

            for (int y = yStart; y < ySize; y++)
            {
                SpriteRenderer render = _tiles[x, y].GetComponent<SpriteRenderer>();

                if (!render.sprite) nullCount++;
                
                renders.Add(render);
            }

            for (int i = 0; i < nullCount; i++)
            {
                yield return new WaitForSeconds(shiftDelay);

                for (int r = 0; r < renders.Count - 1; r++)
                {
                    renders[r].sprite = renders[r + 1].sprite;
                    renders[r + 1].sprite = GetNewSprite(x, ySize -1);
                }
            }

            IsShifting = false;
        }


        private Sprite GetNewSprite(int x, int y)
        {
            List<Sprite> possibleCharacters = new List<Sprite>();
            possibleCharacters.AddRange(tileCharacters);

            if (x > 0)
                possibleCharacters.Remove(_tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
            if(x < xSize - 1)
                possibleCharacters.Remove(_tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
            if(y > 0)
                possibleCharacters.Remove(_tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);

            return possibleCharacters[Random.Range(0, possibleCharacters.Count)];
        }
    }
}
