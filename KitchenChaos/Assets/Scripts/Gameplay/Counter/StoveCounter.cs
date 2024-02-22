using System;
using Gameplay.Audio.Counters;
using Gameplay.KitchenObjects;
using UnityEngine;
using Zenject;

namespace Gameplay.Counter
{

    public class StoveCounter : BaseCounter
    {

        private float _currentFryingTime;
        private float _maxFryingTime;

        private float _currentBurningTime;
        private float _maxBurningTime;

        
        private StoveState _currentState;
        private StoveState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnStateChanged(value);
            }
        }

        private CuttingProgressBarUI _progressBarUI;
        private StoveCounterVisual _stoveCounterVisual;
        private StoveCounterSound _stoveCounterSound;

        [Inject] private KitchenItemSpawner _kitchenItemSpawner;

        [SerializeField]
        private ItemRecipesSO _recipesSo;

        public Action<StoveState> OnStateChanged = delegate { };


        protected override void Awake()
        {
            base.Awake();
            _progressBarUI = GetComponentInChildren<CuttingProgressBarUI>();
            _progressBarUI.gameObject.SetActive(false);
            _stoveCounterVisual = GetComponentInChildren<StoveCounterVisual>();
        }

        private void Start()
        {
            CurrentState = StoveState.Idle;
        }

        private void Update()
        {
            if (!HasKitchenItem()) return;

            switch (CurrentState)
            {
                case StoveState.Idle:
                    break;
                case StoveState.Frying:
                    FryingHandle();
                    break;
                case StoveState.Fried:
                    FriedHandle();
                    break;
                case StoveState.Burned:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FryingHandle()
        {
            _currentFryingTime += Time.deltaTime;
            _progressBarUI.SetImageProgressValue(_currentFryingTime / _maxFryingTime);

            if (_currentFryingTime >= _maxFryingTime)
            {
                ChangeCurrentItemToFriedVersion();
                CurrentState = StoveState.Fried;
                //TODO: play sound
            }
        }

        private void ChangeCurrentItemToFriedVersion()
        {
            var oldItem = _currentKitchenItem;
            if (_recipesSo.HasFryingRecipe(oldItem.ItemType))
            {
                Destroy(oldItem.gameObject);
                ClearKitchenItem();

                var newItemSo = _recipesSo.GetFryingRecipeByType(oldItem.ItemType);
                var newItem = _kitchenItemSpawner.SpawnKitchenItem(newItemSo.FriedItemSo.Prefab, _onTopSpawnPoint);
                SetFriedKitchenItem(newItem);
            }
        }

        private void FriedHandle()
        {
            _currentBurningTime += Time.deltaTime;
            _progressBarUI.SetImageProgressValue(_currentBurningTime / _maxBurningTime);
            if (_currentBurningTime >= _maxBurningTime)
            {
                ChangeToBurnedVersion();
                CurrentState = StoveState.Burned;
                //TODO: stop PLaySound
            }
        }

        private void ChangeToBurnedVersion()
        {
            var oldItem = _currentKitchenItem;
            if (_recipesSo.HasBurningRecipe(oldItem.ItemType))
            {
                Destroy(oldItem.gameObject);
                ClearKitchenItem();

                var newItemSo = _recipesSo.GetBurningRecipeByType(oldItem.ItemType);
                var newItem = _kitchenItemSpawner.SpawnKitchenItem(newItemSo.BurnedItemSo.Prefab, _onTopSpawnPoint);
                base.SetKitchenItem(newItem);
            }
        }

        public override void Interact(IKitchenItemParent player)
        {
            if (!HasKitchenItem())
            {
                if (player.HasKitchenItem() && HasRecipeForPlayerItem(player))
                {
                    PlaceItemFromPlayerToCounter(player);
                    CurrentState = StoveState.Frying;
                    //TODO: play sound
                    _stoveCounterVisual.TurnOnVisualEffects();
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
                        CurrentState = StoveState.Idle;
                        _stoveCounterVisual.TurnOffVisualEffects();
                    }
                }
                else
                {
                    ReturnItemToPlayer(player);
                    CurrentState = StoveState.Idle;
                    _stoveCounterVisual.TurnOffVisualEffects();
                }
            }
        }

        private bool HasRecipeForPlayerItem(IKitchenItemParent kitchenItemParent)
        {
            return _recipesSo.HasFryingRecipe(kitchenItemParent.GetKitchenItem().ItemType);
        }

        private bool CanAddItemToPlate(KitchenItem kitchenItem)
        {
            return kitchenItem is PlateKitchenItem plate && plate.AddIngredient(_currentKitchenItem);
        }

        private void ReturnItemToPlayer(IKitchenItemParent player)
        {
            player.SetKitchenItem(_currentKitchenItem);
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

            if (_recipesSo.HasFryingRecipe(currentKitchenItem.ItemType))
            {
                var fryingRecipeRecipe = _recipesSo.GetFryingRecipeByType(currentKitchenItem.ItemType);
                _currentFryingTime = 0f;
                _maxFryingTime = fryingRecipeRecipe.FryingTimerMax;
                _progressBarUI.gameObject.SetActive(true);
                _progressBarUI.ResetFillAmount();
            }
        }

        public void SetFriedKitchenItem(KitchenItem currentKitchenItem)
        {
            base.SetKitchenItem(currentKitchenItem);

            if (_recipesSo.HasBurningRecipe(currentKitchenItem.ItemType))
            {
                var fryingRecipeRecipe = _recipesSo.GetBurningRecipeByType(currentKitchenItem.ItemType);
                _currentBurningTime = 0f;
                _maxBurningTime = fryingRecipeRecipe.BurningMaxTiming;
                _progressBarUI.ResetFillAmount();
                _progressBarUI.gameObject.SetActive(true);
            }
        }

        public override void ClearKitchenItem()
        {
            base.ClearKitchenItem();
            _progressBarUI.ResetFillAmount();
            _progressBarUI.gameObject.SetActive(false);
        }

    }

}