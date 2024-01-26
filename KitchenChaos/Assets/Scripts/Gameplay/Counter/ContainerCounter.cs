using System;
using Gameplay.KitchenObjects;
using UnityEngine;
using Zenject;

namespace Gameplay.Counter
{

    public class ContainerCounter : BaseCounter
    {

        [SerializeField]
        private KitchenItemSO _kitchenItemSo;

        [Inject] private KitchenItemSpawner _itemSpawner;

        public Action OnKitchenItemSpawn = delegate { };

        public override void Interact(IKitchenItemParent player)
        {
            if (!player.HasKitchenItem())
            {
                KitchenItem kitchenItem = _itemSpawner.SpawnKitchenItem(_kitchenItemSo.Prefab, _onTopSpawnPoint);
                
                player.SetKitchenItem(kitchenItem);
                OnKitchenItemSpawn.Invoke();
            }
        }

        public override void InteractAlternative(IKitchenItemParent playerKitchenItemHolder)
        {
        }
    }

}