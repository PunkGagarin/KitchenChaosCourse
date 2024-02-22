using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.KitchenObjects
{

    public class PlateKitchenItem : KitchenItem
    {


        [SerializeField]
        private List<KitchenItemType> _validTypes;

        public List<KitchenItemType> AddedItemTypes { get; private set; } = new();

        public Action<KitchenItemType> OnKitchenItemAdded = delegate { };

        public bool AddIngredient(KitchenItem item)
        {
            bool hasAdd;

            if (!_validTypes.Contains(item.ItemType) || AddedItemTypes.Contains(item.ItemType))
            {
                hasAdd = false;
            }
            else
            {
                AddedItemTypes.Add(item.ItemType);
                hasAdd = true;

                OnKitchenItemAdded.Invoke(item.ItemType);
            }

            return hasAdd;
        }

        public List<KitchenItemType> GetValidTypes()
        {
            return _validTypes;
        }
    }

}