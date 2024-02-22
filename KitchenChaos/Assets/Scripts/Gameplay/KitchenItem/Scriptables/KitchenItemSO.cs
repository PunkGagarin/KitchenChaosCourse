using UnityEngine;

namespace Gameplay.KitchenObjects
{

    [CreateAssetMenu(menuName = "Gameplay/KitchenItems/KitchenItem", fileName = "new KitchenObject")]
    public class KitchenItemSO : ScriptableObject
    {

        [field: SerializeField]
        public KitchenItem Prefab { get; private set; }

        [field: SerializeField]
        public Sprite Sprite { get; private set; }

        [field: SerializeField]
        public KitchenItemType ItemType { get; private set; }

    }

}