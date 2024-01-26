using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.KitchenObjects
{

    [CreateAssetMenu(menuName = "Gameplay/SlicedItemRecipes", fileName = "new SlicedItemRecipes")]
    public class ItemRecipesSO : ScriptableObject
    {

        [SerializeField]
        private List<CustomKeyValue<KitchenItemType, SliceRecipe>> _slicedRecipes;
        
        [SerializeField]
        private List<CustomKeyValue<KitchenItemType, FryingRecipe>> _fryingRecipes;
        
        [SerializeField]
        private List<CustomKeyValue<KitchenItemType, BurningRecipe>> _burningRecipes;

        public SliceRecipe GetSlicedRecipeByType(KitchenItemType type)
        {
            var slicedSo = _slicedRecipes.FirstOrDefault(el => el.key == type);
            return slicedSo?.value;
        }

        public bool HasSlicedRecipe(KitchenItemType type)
        {
            var slicedSo = _slicedRecipes.FirstOrDefault(el => el.key == type);
            return slicedSo != null;
        }
        
        public FryingRecipe GetFryingRecipeByType(KitchenItemType type)
        {
            var itemSo = _fryingRecipes.FirstOrDefault(el => el.key == type);
            return itemSo?.value;
        }

        public bool HasFryingRecipe(KitchenItemType type)
        {
            var itemSo = _fryingRecipes.FirstOrDefault(el => el.key == type);
            return itemSo != null;
        }
        
        public BurningRecipe GetBurningRecipeByType(KitchenItemType type)
        {
            var itemSo = _burningRecipes.FirstOrDefault(el => el.key == type);
            return itemSo?.value;
        }

        public bool HasBurningRecipe(KitchenItemType type)
        {
            var itemSo = _burningRecipes.FirstOrDefault(el => el.key == type);
            return itemSo != null;
        }

    }

    [Serializable]
    public class SliceRecipe
    {
        [field: SerializeField]
        public KitchenItemSO SlicedItemSo { get; set; }

        [field: SerializeField]
        public int CuttingCounter { get; set; }
    }
    
    [Serializable]
    public class FryingRecipe
    {
        [field: SerializeField]
        public KitchenItemSO FriedItemSo { get; set; }

        [field: SerializeField]
        public float FryingTimerMax { get; set; }
    }
    
    [Serializable]
    public class BurningRecipe
    {
        [field: SerializeField]
        public KitchenItemSO BurnedItemSo { get; set; }

        [field: SerializeField]
        public float BurningMaxTiming { get; set; }
    }

}