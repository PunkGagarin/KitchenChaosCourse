using System.Collections.Generic;
using System.Linq;
using Gameplay.KitchenObjects;
using Gameplay.KitchenObjects.Scriptables;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.Gameplay.KitchenItems
{

    public class PlateIconsUI : MonoBehaviour
    {

        private List<PlateIconSingleUI> _icons = new();

        [SerializeField]
        private KitchenItemSoFactory _soFactory;

        [SerializeField]
        private PlateIconSingleUI _iconPrefab;

        [SerializeField]
        private PlateKitchenItem _plateKitchenItem;

        private void Start()
        {
            InitIcons();
            _plateKitchenItem.OnKitchenItemAdded += OnKitchenItemAddedHandle;
        }

        private void InitIcons()
        {
            var validTypes = _plateKitchenItem.GetValidTypes();
            foreach (var type in validTypes)
            {
                PlateIconSingleUI icon = Instantiate(_iconPrefab, transform);
                icon.Type = type;
                icon.ChangeImageSprite(_soFactory.GetKitchenItemIconByType(type));
                _icons.Add(icon);
                icon.gameObject.SetActive(false);
            }
        }

        private void OnKitchenItemAddedHandle(KitchenItemType type)
        {
            var plateIconSingleUI = _icons.FirstOrDefault(el => el.Type == type);
            if (plateIconSingleUI != null)
            {
                plateIconSingleUI.gameObject.SetActive(true);
            }
        }
    }

}