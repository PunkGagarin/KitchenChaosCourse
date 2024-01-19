using Gameplay.KitchenObjects;
using UnityEngine;

namespace Gameplay.Counter
{

    public abstract class BaseCounter : MonoBehaviour, IKitchenItemParent
    {

        private SelectedCounterVisual _selectedCounterVisual;

        protected KitchenItem _currentKitchenItem;

        [SerializeField]
        protected Transform _onTopSpawnPoint;

        protected virtual void Awake()
        {
            _selectedCounterVisual = GetComponent<SelectedCounterVisual>();
        }

        public abstract void Interact(IKitchenItemParent kitchenItemParent);

        public virtual void SetKitchenItem(KitchenItem currentKitchenItem)
        {
            _currentKitchenItem = currentKitchenItem;
            _currentKitchenItem.transform.parent = _onTopSpawnPoint;
            _currentKitchenItem.SetKitchenItemParent(this);
            _currentKitchenItem.transform.localPosition = Vector3.zero;
        }

        public KitchenItem GetKitchenItem()
        {
            return _currentKitchenItem;
        }

        public virtual void ClearKitchenItem()
        {
            _currentKitchenItem = null; 
        }

        public bool HasKitchenItem()
        {
            return _currentKitchenItem != null;
        }


        public void TurnOnSelectedVisual()
        {
            _selectedCounterVisual.TurnOnSelectedVisual();
        }

        public void TurnOffSelectedVisual()
        {
            _selectedCounterVisual.TurnOffSelectedVisual();
        }

        public abstract void InteractAlternative(IKitchenItemParent playerKitchenItemHolder);
    }

}