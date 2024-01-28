using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.KitchenObjects.Dishes
{

    // [CreateAssetMenu(menuName = "Gameplay/KitchenItems/DishRecipesFactorySO", fileName = "DishRecipesFactory")]
    public class DishRecipesFactorySO : ScriptableObject
    {

        [field: SerializeField]
        public List<DishRecipeSO> recipes { get; set; }
        
    }

}