using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.KitchenObjects
{

    public class PlateCompleteVisual : MonoBehaviour
    {

        [SerializeField]
        private List<CustomKeyValue<KitchenItemType, GameObject>> _completeVisualsForItems;

        [SerializeField]
        private PlateKitchenItem _plateKitchenItem;

        private void Start()
        {
            _plateKitchenItem.OnKitchenItemAdded += OnKitchenItemAddedHandle;

            foreach (var completeVisualPair in _completeVisualsForItems)
            {
                completeVisualPair.value.gameObject.SetActive(false);
            }
        }

        private void OnKitchenItemAddedHandle(KitchenItemType type)
        {
            var pair = _completeVisualsForItems.FirstOrDefault(el => el.key == type);
            pair?.value.gameObject.SetActive(true);
        }

    }

}