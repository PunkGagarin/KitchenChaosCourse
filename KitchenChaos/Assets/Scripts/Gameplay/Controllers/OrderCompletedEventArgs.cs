using System;
using Gameplay.KitchenObjects;
using UnityEngine;

namespace Gameplay.Controllers
{

    public class OrderCompletedEventArgs : EventArgs
    {
        public RecipeType recipeType;
        public Transform transform;
    }

}