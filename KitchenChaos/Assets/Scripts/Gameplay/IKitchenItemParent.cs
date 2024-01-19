using Gameplay.KitchenObjects;
using UnityEngine;

namespace Gameplay
{

    public interface IKitchenItemParent
    {
        // public Transform GetKitchenItemFollowTransform();
        public void SetKitchenItem(KitchenItem kitchenItem);
        public KitchenItem GetKitchenItem();
        public void ClearKitchenItem();
        public bool HasKitchenItem();

        // public  KitchenItem KitchenItem { get; set; }
    }

}