using UnityEngine;

namespace Gameplay.KitchenObjects
{

    public class KitchenItemSpawner
    {

        public KitchenItem SpawnKitchenItem(KitchenItem prefab, Transform parentTransform)
        {
            KitchenItem kitchenItem = GameObject.Instantiate(prefab, parentTransform.position,
                Quaternion.identity, parentTransform);
            return kitchenItem;
        }
    }

}