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
        
        private void Start()
        {
            _render = GetComponent<SpriteRenderer>();
            name = _render.sprite.name;
        }


        private void Select()
        {
            _isSelected = true;
            _render.color = _selectedColour;
            _previousSelected = gameObject.GetComponent<Tile>();
        }

        
        private void Deselect()
        {
            _isSelected = false;
            _render.color = Color.white;
            _previousSelected = null;
        }

        
        private void OnMouseDown()
        {
            if (!_render.sprite || BoardManager.Instance.IsShifting)  return;
            
            if(_isSelected)
                Deselect();
            else
            {
                if(!_previousSelected) Select();

                else
                {
                    if (GetAllAdjacentTiles().Contains(_previousSelected.gameObject))
                    {
                        SwapTiles(_previousSelected._render);
                        ClearAllMatches();
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

        
        private void SwapTiles(SpriteRenderer swapRender)
        {
            if (_render.sprite == swapRender.sprite) return;

            Sprite tempSprite = _render.sprite;
            _render.sprite = swapRender.sprite;
            swapRender.sprite = tempSprite;

            foreach (Vector2 dir in _adjacentDirections)
            {
                FindMatch(dir);
            }

            GUIManager.Instance.MovesCounter -= 1;
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

            _matchFound = true;
            GUIManager.Instance.Score += 50;
        }


        public void ClearAllMatches()
        {
            if (!_render.sprite) return;
            
            ClearMatch(new Vector2[] {Vector2.left, Vector2.right});
            ClearMatch(new Vector2[] {Vector2.up, Vector2.down});

            if (!_matchFound) return;
            
            _render.sprite = null;
            _matchFound = false;
            
            StopCoroutine(BoardManager.Instance.FindNullTiles());
            StartCoroutine(BoardManager.Instance.FindNullTiles());
        }
    }
}
