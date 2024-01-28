using Gameplay.Controllers;
using Gameplay.KitchenObjects;
using Zenject;

namespace Gameplay.Counter
{

    public class DeliveryCounter : BaseCounter
    {

        [Inject] private DeliveryManager _deliveryManager;
        public override void Interact(IKitchenItemParent player)
        {
            if (player.HasKitchenItem() && player.GetKitchenItem() is PlateKitchenItem)
            {
                _deliveryManager.DeliverRecipe(player.GetKitchenItem() as PlateKitchenItem);
                player.ClearWithDestroy();
            }
        }
    }

}