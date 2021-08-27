using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using Managers;
using UnityEngine;

namespace Flowers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlowerController : MonoBehaviour
    {
        [SerializeField] private Flower flowerType;
        public Flower FlowerType => flowerType;

        private int _costToBuy;
        private int _pollenProduction;
        [SerializeField] private SpriteRenderer _sprite;

        [SerializeField] private float _producePollenTime;
        private float _pollenTimer;

        private int _level = 1;

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                _pollenProduction += flowerType.pollenIncrease;
            }
        }

        private void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _sprite.sprite = flowerType.flowerSprite;

            if (_level == 1) _pollenProduction = flowerType.startingPollen;
        }

        private void Update()
        {
            IdleProducePollen();
        }

        private void OnMouseDown()
        {
            Debug.Log("Clicked");
            ProducePollen();
        }

        private void IdleProducePollen()
        {
            if (_pollenTimer >= _producePollenTime)
            {
                ProducePollen();
                _pollenTimer = 0;
                return;
            }
            
            _pollenTimer += Time.deltaTime;
        }

        private void ProducePollen()
        {
            if (!IdleManager.Instance)
                return;
            
            IdleManager.Instance.TotalPollen += _pollenProduction;
        }
        
    }
}
