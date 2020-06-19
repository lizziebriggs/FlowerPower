using System.Collections.Generic;
using Managers;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace MatchThree
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        private bool _isSelected;
        private static Tile _previousSelected;

        private SpriteRenderer _render;
        private readonly Color _selectedColour = new Color(.5f, .5f, .5f, 1.0f);
        
        private readonly Vector2[] _adjacentDirections = new Vector2[]{Vector2.up, Vector2.down, Vector2.left, Vector2.right};

        private bool _matchFound;

        [SerializeField] private Animator animator;
        
        [Header("Selection")]
        [SerializeField] private float swipeThreshold = 20f;
        public bool detectSwipeOnlyAfterRelease;
        private Vector2 _fingerDown;
        private Vector2 _fingerUp;

        
        private void Start()
        {
            _render = GetComponent<SpriteRenderer>();
            animator.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (!_isSelected) return;
        
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _fingerUp = touch.position;
                    _fingerDown = touch.position;
                }
        
                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    _fingerDown = touch.position;
                    _previousSelected = CheckSwipe().GetComponent<Tile>();
                    
                    SwapTiles(_previousSelected._render);
                    ClearAllMatches();
                    _previousSelected.GetComponent<Tile>().ClearAllMatches(); // Clear matches for other swapped tile
                    
                    _previousSelected.Deselect();
                    Deselect();
                }
            }
        }

        // For PC testing
        private void OnMouseDown()
        {
            if (!_render.sprite || BoardManager.Instance.IsShifting)  return;
            
            if(_isSelected)
                Deselect();
            else
            {
                if (!_previousSelected)
                {
                    Select();
                    Debug.Log(name + " selected");
                }

                else
                {
                    if (GetAllAdjacentTiles().Contains(_previousSelected.gameObject))
                    {
                        SwapTiles(_previousSelected._render);
                        ClearAllMatches();
                        _previousSelected.GetComponent<Tile>().ClearAllMatches(); // Clear matches for other swapped tile
                        _previousSelected.Deselect();
                    }
                    else
                    {
                        _previousSelected.ClearAllMatches();
                        _previousSelected.Deselect();
                        Select();
                    }
                }
            }
        }


        private void Select()
        {
            _isSelected = true;
            //_render.color = _selectedColour;
            animator.gameObject.SetActive(true);
            _previousSelected = gameObject.GetComponent<Tile>();
        }

        
        private void Deselect()
        {
            _isSelected = false;
            //_render.color = Color.white;
            animator.gameObject.SetActive(false);
            _previousSelected = null;
        }
        
        private GameObject CheckSwipe()
        {
            GameObject swappingTile = null;
            
            // Vertical swipes
            if (VerticalMove() > swipeThreshold && VerticalMove() > HorizontalValMove())
            {
                if (_fingerDown.y - _fingerUp.y > 0)//up swipe
                {
                    //Debug.Log("Swipe Up");
                    swappingTile = GetAdjacentTile(_adjacentDirections[0]);
                }
                else if (_fingerDown.y - _fingerUp.y < 0)//Down swipe
                {
                    //Debug.Log("Swipe Down");
                    swappingTile = GetAdjacentTile(_adjacentDirections[1]);
                }
                _fingerUp = _fingerDown;
            }

            // Horizontal swipes
            else if (HorizontalValMove() > swipeThreshold && HorizontalValMove() > VerticalMove())
            {
                if (_fingerDown.x - _fingerUp.x < 0) // Left swipe
                {
                    //Debug.Log("Swipe Left");
                    swappingTile = GetAdjacentTile(_adjacentDirections[2]);
                }
                else if (_fingerDown.x - _fingerUp.x > 0) // Right swipe
                {
                    //Debug.Log("Swipe Right");
                    swappingTile = GetAdjacentTile(_adjacentDirections[3]);
                }
                
                _fingerUp = _fingerDown;
            }

            return swappingTile;
        }
        
        private float VerticalMove()
        {
            return Mathf.Abs(_fingerDown.y - _fingerUp.y);
        }

        private float HorizontalValMove()
        {
            return Mathf.Abs(_fingerDown.x - _fingerUp.x);
        }

        private void SwapTiles(SpriteRenderer swapRender)
        {
            if (_render.sprite == swapRender.sprite) return;

            Sprite tempSprite = _render.sprite;
            _render.sprite = swapRender.sprite;
            swapRender.sprite = tempSprite;
            
            //Debug.Log(name + " swapped");

            foreach (Vector2 dir in _adjacentDirections)
            {
                FindMatch(dir);
                swapRender.GetComponent<Tile>().FindMatch(dir);
            }


            GuiManager.Instance.MovesCount -= 1;
        }

        
        private GameObject GetAdjacentTile(Vector2 castDir)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
            return hit.collider ? hit.collider.gameObject : null; // If ray cast hits return object o/w return null
        }


        private List<GameObject> GetAllAdjacentTiles()
        {
            List<GameObject> adjacentTiles = new List<GameObject>();

            foreach (Vector2 dir in _adjacentDirections)
            {
                adjacentTiles.Add(GetAdjacentTile(dir));
            }
            
            return adjacentTiles;
        }


        private List<GameObject> FindMatch(Vector2 castDir)
        {
            //Debug.Log(name + " searching for matches");
            
            List<GameObject> matchingTiles = new List<GameObject>();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);

            while (hit.collider && hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == _render.sprite)
            {
                matchingTiles.Add(hit.collider.gameObject);
                hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
            }

            return matchingTiles;
        }


        private void ClearMatch(Vector2[] paths)
        {
            List<GameObject> matchingTiles = new List<GameObject>();

            foreach (Vector2 path in paths)
            {
                matchingTiles.AddRange(FindMatch(path));
            }

            if (matchingTiles.Count < 2) return;
            
            foreach (GameObject tile in matchingTiles)
            {
                tile.GetComponent<SpriteRenderer>().sprite = null;
            }
            
            // Add appropriate score
            switch (matchingTiles.Count)
            {
                case 2: 
                    GuiManager.Instance.Score += MatchThreeManager.Instance.ThreeTileMatch;
                    break;
                
                case 3: 
                    GuiManager.Instance.Score += MatchThreeManager.Instance.FourTileMatch;
                    break;
                
                case 4: 
                    GuiManager.Instance.Score += MatchThreeManager.Instance.FiveTileMatch;
                    break;
                
                default:
                    GuiManager.Instance.Score += 50;
                    break;
            }

            //Debug.Log(name + " matches found");
            SfxManager.Instance.tilesMatch.Play();
            _matchFound = true;
        }


        public void ClearAllMatches()
        {
            if (!_render.sprite) return;
            
            ClearMatch(new [] {Vector2.left, Vector2.right});
            ClearMatch(new [] {Vector2.up, Vector2.down});

            if (!_matchFound) return;
            
            _render.sprite = null;
            _matchFound = false;
            
            StopCoroutine(BoardManager.Instance.FindNullTiles());
            StartCoroutine(BoardManager.Instance.FindNullTiles());
        }
    }
}
