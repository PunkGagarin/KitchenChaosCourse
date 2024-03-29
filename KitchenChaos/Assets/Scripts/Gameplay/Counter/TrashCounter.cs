﻿using Gameplay.Audio;

namespace Gameplay.Counter
{

    public class TrashCounter : BaseCounter
    {

        public override void Interact(IKitchenItemParent player)
        {
            if (player.HasKitchenItem())
            {
                var ki = player.GetKitchenItem();
                Destroy(ki.gameObject);
                player.ClearKitchenItem();
                _soundManager.PlayRandomSoundByType(GameAudioType.Trash, transform);
            }
        }
    }

}