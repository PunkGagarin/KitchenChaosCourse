using Gameplay.Counter;
using UnityEngine;

namespace Gameplay.KitchenObjects
{

    public class KitchenItem : MonoBehaviour
    {


        private EmptyCounter _currentCounter;

        private IKitchenItemParent _kitchenItemParent;

        [field: SerializeField]
        public KitchenItemType ItemType { get; private set; }

        public void SetEmptyCounter(EmptyCounter counter)
        {
            _currentCounter = counter;
        }

        public void SetKitchenItemParent(IKitchenItemParent itemParent)
        {
            _kitchenItemParent = itemParent;
        }
    }

}