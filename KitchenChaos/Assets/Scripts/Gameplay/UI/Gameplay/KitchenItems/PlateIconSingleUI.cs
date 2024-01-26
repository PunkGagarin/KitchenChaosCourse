using Gameplay.KitchenObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Gameplay.KitchenItems
{

    public class PlateIconSingleUI : MonoBehaviour
    {

        public KitchenItemType Type { get; set; }

        [SerializeField]
        private Image _iconImage;

        public void ChangeImageSprite(Sprite icon)
        {
            _iconImage.sprite = icon;
        }


    }

}