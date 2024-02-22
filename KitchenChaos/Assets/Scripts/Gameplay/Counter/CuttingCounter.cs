using System;
using Gameplay.Audio;
using Gameplay.KitchenObjects;
using UnityEngine;
using Zenject;

namespace Gameplay.Counter
{

    public class CuttingCounter : BaseCounter
    {

        private int _currentSliceCounter;
        private int _currentMaxSliceCount;

        private CuttingProgressBarUI _progressBarUI;
        private CuttingCounterAnimation _cuttingCounterAnimation;

        [Inject] private KitchenItemSpawner _kitchenItemSpawner;

        [SerializeField]
        private ItemRecipesSO _recipesSo;

        protected override void Awake()
        {
            base.Awake();
            _progressBarUI = GetComponentInChildren<CuttingProgressBarUI>();
            _progressBarUI.gameObject.SetActive(false);
            _cuttingCounterAnimation = GetComponentInChildren<CuttingCounterAnimation>();
        }

        public override void Interact(IKitchenItemParent player)
        {
            if (!HasKitchenItem())
            {
                if (player.HasKitchenItem() && HasRecipeForPlayerItem(player))
                {
                    PlaceItemFromPlayerToCounter(player);
                }
            }
            else
            {
                if (player.HasKitchenItem())
                {
                    var kitchenItem = player.GetKitchenItem();
                    if (CanAddItemToPlate(kitchenItem))
                    {
                        ClearWithDestroy();
                    }
                }
                else
                {
                    ReturnItemToPlayer(player);
                }
            }
        }

        private bool HasRecipeForPlayerItem(IKitchenItemParent kitchenItemParent)
        {
            return _recipesSo.HasSlicedRecipe(kitchenItemParent.GetKitchenItem().ItemType);
        }

        private bool CanAddItemToPlate(KitchenItem kitchenItem)
        {
            return kitchenItem is PlateKitchenItem plate && plate.AddIngredient(_currentKitchenItem);
        }

        private void ReturnItemToPlayer(IKitchenItemParent kitchenItemParent)
        {
            kitchenItemParent.SetKitchenItem(_currentKitchenItem);
            ClearKitchenItem();
        }

        private void PlaceItemFromPlayerToCounter(IKitchenItemParent kitchenItemParent)
        {
            SetKitchenItem(kitchenItemParent.GetKitchenItem());
            kitchenItemParent.ClearKitchenItem();
        }

        public override void SetKitchenItem(KitchenItem currentKitchenItem)
        {
            base.SetKitchenItem(currentKitchenItem);
            var sliceRecipe = _recipesSo.GetSlicedRecipeByType(currentKitchenItem.ItemType);
            _currentMaxSliceCount = sliceRecipe.CuttingCounter;
            _currentSliceCounter = 0;
            _progressBarUI.gameObject.SetActive(true);
            _progressBarUI.ResetFillAmount();
        }

        public override void InteractAlternative(IKitchenItemParent playerKitchenItemHolder)
        {
            if (_currentKitchenItem == null) return;

            var type = _currentKitchenItem.ItemType;
            if (_recipesSo.HasSlicedRecipe(type) && !HasReachMaxSliceCounter())
            {
                _currentSliceCounter++;
                _progressBarUI.SetImageProgressValue((float)(_currentSliceCounter) / _currentMaxSliceCount);
                _cuttingCounterAnimation.PlayOpenAnimation();
                _soundManager.PlayRandomSoundByType(GameAudioType.Chop ,transform);

                if (HasReachMaxSliceCounter())
                {
                    var recipe = _recipesSo.GetSlicedRecipeByType(type);

                    Destroy(_currentKitchenItem.gameObject);

                    ClearKitchenItem();

                    KitchenItem kitchenItem =
                        _kitchenItemSpawner.SpawnKitchenItem(recipe.SlicedItemSo.Prefab, _onTopSpawnPoint);
                    base.SetKitchenItem(kitchenItem);
                }
            }
        }

        public override void ClearKitchenItem()
        {
            base.ClearKitchenItem();
            _progressBarUI.gameObject.SetActive(false);
        }

        private bool HasReachMaxSliceCounter()
        {
            return _currentSliceCounter >= _currentMaxSliceCount;
        }
    }

}