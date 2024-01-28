using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.KitchenObjects.Scriptables
{

    //[CreateAssetMenu(menuName = "Gameplay/KitchenItems/KitchenItemsSoFactory", fileName = "KitchenItemSoFactory")]
    public class KitchenItemSoFactory : ScriptableObject
    {

        private List<KitchenItemSO> _kitchenItemSos;


        public KitchenItemSO GetKitchenItemSoByType(KitchenItemType type)
        {
            InitIfNeeded();
            var item = _kitchenItemSos.FirstOrDefault(el => el.ItemType == type);
            return item;
        }

        public Sprite GetKitchenItemIconByType(KitchenItemType type)
        {
            InitIfNeeded();
            var item = _kitchenItemSos.FirstOrDefault(el => el.ItemType == type);
            return item != null ? item.Sprite : null;
        }

        private void InitIfNeeded()
        {
            if (_kitchenItemSos.Count <= 0)
            {
                _kitchenItemSos = Resources.LoadAll<KitchenItemSO>("KitchenObjects").ToList();
            }
        }
    }

}