using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.KitchenObjects
{

    [CreateAssetMenu(menuName = "Gameplay/KitchenItems/DishRecipe", fileName = "DishRecipe")]
    public class DishRecipeSO : ScriptableObject
    {

        [field: SerializeField]
        public List<KitchenItemType> Ingredients { get; private set; }


        [field: SerializeField]
        public RecipeType Type { get; private set; }

        public bool MatchIngredients(List<KitchenItemType> ingredients)
        {
            if (ingredients.Count != Ingredients.Count) return false;

            return ingredients.Select(plateIngredientType =>
                    Ingredients.Any(recipeIngredientType => plateIngredientType == recipeIngredientType))
                .All(isIngredientMatch => isIngredientMatch);
        }

    }

}