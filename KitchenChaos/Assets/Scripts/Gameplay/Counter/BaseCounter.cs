using Gameplay.Audio;
using Gameplay.KitchenObjects;
using UnityEngine;
using Zenject;

namespace Gameplay.Counter
{

    public abstract class BaseCounter : MonoBehaviour, IKitchenItemParent
    {

        private SelectedCounterVisual _selectedCounterVisual;

        protected KitchenItem _currentKitchenItem;

        [Inject] protected SoundManager _soundManager;

        [SerializeField]
        protected Transform _onTopSpawnPoint;


        protected virtual void Awake()
        {
            _selectedCounterVisual = GetComponent<SelectedCounterVisual>();
        }

        public abstract void Interact(IKitchenItemParent player);

        public virtual void SetKitchenItem(KitchenItem currentKitchenItem)
        {
            if (currentKitchenItem == null) return;

            _currentKitchenItem = currentKitchenItem;
            _currentKitchenItem.transform.parent = _onTopSpawnPoint;
            _currentKitchenItem.SetKitchenItemParent(this);
            _currentKitchenItem.transform.localPosition = Vector3.zero;
            _soundManager.PlayRandomSoundByType(GameAudioType.ItemDrop, transform);
        }

        public KitchenItem GetKitchenItem()
        {
            return _currentKitchenItem;
        }

        public virtual void ClearKitchenItem()
        {
            _currentKitchenItem = null;
        }

        public void ClearWithDestroy()
        {
            Destroy(_currentKitchenItem.gameObject);
            ClearKitchenItem();
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

        public virtual void InteractAlternative(IKitchenItemParent playerKitchenItemHolder)
        {
        }
    }

}