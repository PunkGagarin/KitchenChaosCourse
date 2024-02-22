using System;
using System.Collections.Generic;
using Gameplay.KitchenObjects;
using Gameplay.KitchenObjects.Dishes;
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
        private DishRecipesFactorySO _recipesFactory;

        public int SuccessDeliveryCount { get; private set; }

        public Action<RecipeType> OnOrderRecieved = delegate { };
        public Action<OrderCompletedEventArgs> OnOrderCompleted = delegate { };
        public Action<Transform> OnOrderFailed = delegate { };


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
                    _waitingDishRecipeSos.Remove(recipe);
                    SuccessDeliveryCount++;
                    OnOrderCompleted.Invoke(new OrderCompletedEventArgs
                        { recipeType = recipe.Type, transform = transform });


                    return;
                }
            }

            OnOrderFailed.Invoke(transform);
        }
    }

}