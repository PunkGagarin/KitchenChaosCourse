using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.KitchenObjects
{

    [CreateAssetMenu(menuName = "Gameplay/SlicedItemRecipes", fileName = "new SlicedItemRecipes")]
    public class SlicedItemRecipesSO : ScriptableObject
    {

        [SerializeField]
        private List<CustomKeyValue<KitchenItemType, SliceRecipe>> _slicedRecipes;

        public SliceRecipe GetRecipeByType(KitchenItemType type)
        {
            var slicedSo = _slicedRecipes.FirstOrDefault(el => el.key == type);
            return slicedSo?.value;
        }

        public bool HasRecipe(KitchenItemType type)
        {
            var slicedSo = _slicedRecipes.FirstOrDefault(el => el.key == type);
            return slicedSo != null;
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

}