using Gameplay.KitchenObjects;
using UnityEngine;

namespace Gameplay
{

    public interface IKitchenItemParent
    {
        public void SetKitchenItem(KitchenItem kitchenItem);
        public KitchenItem GetKitchenItem();
        public void ClearKitchenItem();
        public void ClearWithDestroy();
        public bool HasKitchenItem();
    }

}