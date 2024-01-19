using Gameplay.KitchenObjects;
using UnityEngine;

namespace Gameplay.Player
{

    public class PlayerKitchenItemHolder : MonoBehaviour, IKitchenItemParent
    {
        private KitchenItem _currentKitchenItem;

        [SerializeField]
        private Transform _kitchenItemHoldingPoint;
        
        public void SetKitchenItem(KitchenItem currentKitchenItem)
        {
            _currentKitchenItem = currentKitchenItem;
            _currentKitchenItem.transform.parent = _kitchenItemHoldingPoint;
            _currentKitchenItem.SetKitchenItemParent(this);
            _currentKitchenItem.transform.localPosition = Vector3.zero;
        }

        public Transform GetKitchenItemFollowTransform()
        {
            return _kitchenItemHoldingPoint;
        }

        public KitchenItem GetKitchenItem()
        {
            return _currentKitchenItem;
        }

        public void ClearKitchenItem()
        {
            _currentKitchenItem = null;
        }

        public bool HasKitchenItem()
        {
            return _currentKitchenItem != null;
        }
    }

}