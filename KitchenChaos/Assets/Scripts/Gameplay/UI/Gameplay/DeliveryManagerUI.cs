using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Controllers;
using Gameplay.KitchenObjects;
using Gameplay.KitchenObjects.Dishes;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.Gameplay
{

    public class DeliveryManagerUI : MonoBehaviour
    {
        private List<RecipeTemplateUI> _recipeUis = new();

        [Inject] private DeliveryManager _deliveryManager;
        
        [SerializeField]
        private Transform _content;

        [SerializeField]
        private RecipeTemplateUI _recipePrefab;

        [SerializeField]
        private DishRecipesFactorySO _dishRecipesFactorySo;

        private void Awake()
        {
            CreateUI();
        }

        private void CreateUI()
        {
            var types = _dishRecipesFactorySo.recipes;
            foreach (var recipe in types)
            {
                RecipeTemplateUI recipeUi = Instantiate(_recipePrefab, _content);
                recipeUi.Init(recipe.Type, recipe.Type.ToString());
                _recipeUis.Add(recipeUi);
                recipeUi.gameObject.SetActive(false);
            }
        }


        private void Start()
        {
            _deliveryManager.OnOrderRecieved += OnOrderReceived;
            _deliveryManager.OnOrderCompleted += OnOrderCompleted;
        }

        private void OnDestroy()
        {
            _deliveryManager.OnOrderRecieved -= OnOrderReceived;
            _deliveryManager.OnOrderCompleted -= OnOrderCompleted;
        }

        private void OnOrderReceived(RecipeType recipeType)
        {
            TurnOnRecipeUiFor(recipeType);
        }

        private void TurnOnRecipeUiFor(RecipeType recipeType)
        {
            var recipeTemplateUI = _recipeUis.FirstOrDefault(el => !el.gameObject.activeSelf);
            if (recipeTemplateUI != null)
            {
                recipeTemplateUI.gameObject.SetActive(true);
                recipeTemplateUI.transform.SetSiblingIndex(0);
                recipeTemplateUI.InitWithSpawn(recipeType, recipeType.ToString(), _dishRecipesFactorySo.recipes.FirstOrDefault(el => el.Type == recipeType)?.Ingredients);
            }
        }

        private void OnOrderCompleted(OrderCompletedEventArgs orderCompletedEventArgs)
        {
            TurnOffRecipeUiFor(orderCompletedEventArgs.recipeType);
        }

        private void TurnOffRecipeUiFor(RecipeType recipeType)
        {
            _recipeUis.FirstOrDefault(el => el.Type == recipeType)?.gameObject.SetActive(false);
        }
    }

}