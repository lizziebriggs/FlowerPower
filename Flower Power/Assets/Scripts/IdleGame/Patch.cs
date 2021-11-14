using System;
using Flowers;
using Managers;
using UnityEngine;

namespace IdleGame
{
    public class Patch : MonoBehaviour
    {
        [SerializeField] private FlowerPatch flowerPatch;

        [SerializeField] private Sprite addFlowerSprite;
        [SerializeField] private GameObject shop;
        
        [SerializeField] private SpriteRenderer patchSprite;
        public SpriteRenderer PatchSprite
        {
            get => patchSprite;
            set => patchSprite = value;
        }

        private FlowerController flower;
        public FlowerController Flower
        {
            get => flower;
            set => flower = value;
        }

        private bool hasFlower;
        public bool HasFlower
        {
            get => hasFlower;
            set => hasFlower = value;
        }

        private void Awake()
        {
            if (flowerPatch.CurrentState == FlowerPatch.State.Idle && !hasFlower)
                patchSprite.sprite = null;
        }

        private void Update()
        {
            switch (flowerPatch.CurrentState)
            {
                case FlowerPatch.State.Idle:
                    patchSprite.sprite = null;
                    break;
                
                case FlowerPatch.State.Purchasing:
                    if (!hasFlower)
                        patchSprite.sprite = addFlowerSprite;
                    break;
            }
        }

        private void OnMouseDown()
        {
            switch (flowerPatch.CurrentState)
            {
                case FlowerPatch.State.Purchasing:
                    PlantFlower(FlowerShopManager.Instance.RequestedFlowerToBuy);
                    break;
            }
        }

        private void PlantFlower(FlowerController flowerToPlant)
        {
            if (hasFlower)
                return;
            
            flower = Instantiate(flowerToPlant, transform.position, Quaternion.Euler(0, 0, 0));
            hasFlower = true;
            IdleManager.Instance.TotalPollen -= flowerToPlant.FlowerType.startingPollen;
                
            foreach (FlowerPatch flowerPatch in FlowerShopManager.Instance.FlowerPatches)
            {
                flowerPatch.CurrentState = FlowerPatch.State.Idle;
            }
        }
    }
}
