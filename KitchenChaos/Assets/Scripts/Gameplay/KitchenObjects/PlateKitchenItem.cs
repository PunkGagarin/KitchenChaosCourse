using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.KitchenObjects
{

    public class PlateKitchenItem : KitchenItem
    {

        private List<KitchenItemType> _addedItemTypes = new();

        [SerializeField]
        private List<KitchenItemType> _validTypes;

        public Action<KitchenItemType> OnKitchenItemAdded = delegate { };

        public bool AddIngredient(KitchenItem item)
        {
            bool hasAdd;

            if (!_validTypes.Contains(item.ItemType) || _addedItemTypes.Contains(item.ItemType))
            {
                hasAdd = false;
            }
            else
            {
                _addedItemTypes.Add(item.ItemType);
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