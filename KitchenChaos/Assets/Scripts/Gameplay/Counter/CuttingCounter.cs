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
        private SlicedItemRecipesSO _recipesSo;

        protected override void Awake()
        {
            base.Awake();
            _progressBarUI = GetComponentInChildren<CuttingProgressBarUI>();
            _progressBarUI.gameObject.SetActive(false);
            _cuttingCounterAnimation = GetComponentInChildren<CuttingCounterAnimation>();
        }


        public override void Interact(IKitchenItemParent kitchenItemParent)
        {
            if (!HasKitchenItem())
            {
                if (kitchenItemParent.HasKitchenItem() && HasRecipeForPlayerItem(kitchenItemParent))
                {
                    PlaceItemFromPlayerToCounter(kitchenItemParent);
                }
            }
            else
            {
                if (!kitchenItemParent.HasKitchenItem())
                {
                    ReturnItemToPlayer(kitchenItemParent);
                }
            }
        }

        private bool HasRecipeForPlayerItem(IKitchenItemParent kitchenItemParent)
        {
            return _recipesSo.HasRecipe(kitchenItemParent.GetKitchenItem().ItemType);
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
            var sliceRecipe = _recipesSo.GetRecipeByType(currentKitchenItem.ItemType);
            _currentMaxSliceCount = sliceRecipe.CuttingCounter;
            _currentSliceCounter = 0;
            _progressBarUI.gameObject.SetActive(true);
            _progressBarUI.ResetFillAmount();
        }

        public override void InteractAlternative(IKitchenItemParent playerKitchenItemHolder)
        {
            if (_currentKitchenItem == null) return;

            var type = _currentKitchenItem.ItemType;
            if (_recipesSo.HasRecipe(type) && !HasReachMaxSliceCounter())
            {
                _currentSliceCounter++;
                _progressBarUI.SetImageProgressValue((float)(_currentSliceCounter) / _currentMaxSliceCount);
                _cuttingCounterAnimation.PlayOpenAnimation();

                if (HasReachMaxSliceCounter())
                {
                    var recipe = _recipesSo.GetRecipeByType(type);

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