using System;
using Gameplay.KitchenObjects;
using UnityEngine;

namespace Gameplay.Player
{

    public class PlayerKitchenItemHolder : MonoBehaviour, IKitchenItemParent
    {
        private KitchenItem _currentKitchenItem;

        [SerializeField]
        private Transform _kitchenItemHoldingPoint;

        public Action<Transform> OnSetKitchenItem = delegate { };

        public void SetKitchenItem(KitchenItem currentKitchenItem)
        {
            if (currentKitchenItem == null) return;

            _currentKitchenItem = currentKitchenItem;
            _currentKitchenItem.transform.parent = _kitchenItemHoldingPoint;
            _currentKitchenItem.SetKitchenItemParent(this);
            _currentKitchenItem.transform.localPosition = Vector3.zero;
            OnSetKitchenItem.Invoke(transform);
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

        public void ClearWithDestroy()
        {
            Destroy(_currentKitchenItem.gameObject);
            _currentKitchenItem = null;
        }

        public bool HasKitchenItem()
        {
            return _currentKitchenItem != null;
        }
    }

}