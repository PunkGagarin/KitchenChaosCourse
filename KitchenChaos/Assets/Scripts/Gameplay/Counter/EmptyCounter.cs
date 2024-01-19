namespace Gameplay.Counter
{

    public class EmptyCounter : BaseCounter
    {

        public override void Interact(IKitchenItemParent kitchenItemParent)
        {
            if (!HasKitchenItem())
            {
                if (kitchenItemParent.HasKitchenItem())
                {
                    SetKitchenItem(kitchenItemParent.GetKitchenItem());
                    kitchenItemParent.ClearKitchenItem();
                }
            }
            else
            {
                if (!kitchenItemParent.HasKitchenItem())
                {
                    kitchenItemParent.SetKitchenItem(_currentKitchenItem);
                    ClearKitchenItem();
                }
            }
        }

        public override void InteractAlternative(IKitchenItemParent playerKitchenItemHolder)
        {
        }
    }


}