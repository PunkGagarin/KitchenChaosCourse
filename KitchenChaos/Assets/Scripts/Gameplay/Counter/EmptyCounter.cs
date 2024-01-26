using Gameplay.KitchenObjects;

namespace Gameplay.Counter
{

    public class EmptyCounter : BaseCounter
    {

        public override void Interact(IKitchenItemParent player)
        {
            if (!HasKitchenItem())
            {
                if (player.HasKitchenItem())
                {
                    SetKitchenItem(player.GetKitchenItem());
                    player.ClearKitchenItem();
                }
            }
            else
            {
                if (player.HasKitchenItem())
                {
                    var kitchenItem = player.GetKitchenItem();
                    if (CanAddItemToPlayerPlate(kitchenItem))
                    {
                        ClearWithDestroy();
                    }
                    else if (CanAddItemToPlateOnCounter(kitchenItem))
                    {
                        player.ClearWithDestroy();
                    }
                }
                else
                {
                    player.SetKitchenItem(_currentKitchenItem);
                    ClearKitchenItem();
                }
            }
        }

        private bool CanAddItemToPlateOnCounter(KitchenItem kitchenItem)
        {
            return _currentKitchenItem is PlateKitchenItem plate && plate.AddIngredient(kitchenItem);
        }

        private bool CanAddItemToPlayerPlate(KitchenItem kitchenItem)
        {
            return kitchenItem is PlateKitchenItem plate && plate.AddIngredient(_currentKitchenItem);
        }
    }


}