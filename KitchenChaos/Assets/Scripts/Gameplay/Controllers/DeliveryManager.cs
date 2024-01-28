using System;
using System.Collections.Generic;
using Gameplay.KitchenObjects;
using Gameplay.KitchenObjects.Dishes;
using ModestTree.Util;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Controllers
{

    public class DeliveryManager : MonoBehaviour
    {

        private List<DishRecipeSO> _waitingDishRecipeSos = new();

        private float _currentSpawnTimer;

        [SerializeField]
        private float _spawnTimerMax = 5f;

        [SerializeField]
        private int _waitingRecipesMax = 4;

        [SerializeField]
        private Transform _deliveryContainer;

        [SerializeField]
        private DishRecipesFactorySO _recipesFactory;

        public Action<RecipeType> OnOrderRecieved = delegate { };
        public Action<RecipeType> OnOrderCompleted = delegate { };


        private void Start()
        {
            _currentSpawnTimer = _spawnTimerMax;
        }

        private void Update()
        {
            CreateRecipe();
        }

        private void CreateRecipe()
        {
            if (_waitingDishRecipeSos.Count >= _waitingRecipesMax) return;

            _currentSpawnTimer -= Time.deltaTime;
            if (_currentSpawnTimer <= 0)
            {
                _currentSpawnTimer = _spawnTimerMax;
                DishRecipeSO soToSpawn = _recipesFactory.recipes[Random.Range(0, _recipesFactory.recipes.Count)];
                _waitingDishRecipeSos.Add(soToSpawn);
                OnOrderRecieved.Invoke(soToSpawn.Type);
                Debug.Log(soToSpawn.Type);
            }
        }

        public void DeliverRecipe(PlateKitchenItem plate)
        {
            var plateIngredients = plate.AddedItemTypes;

            foreach (var recipe in _waitingDishRecipeSos)
            {
                bool isRecipeMatch = recipe.MatchIngredients(plateIngredients);

                if (isRecipeMatch)
                {
                    Debug.Log("Delivered " + recipe.name);
                    _waitingDishRecipeSos.Remove(recipe);
                    OnOrderCompleted.Invoke(recipe.Type);
                    return;
                }
            }
            Debug.Log("Recipe not found");
        }
    }

}