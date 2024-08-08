using System;
using UnityEngine;

namespace Gameplay
{
    public interface IGameInputManager
    {
        Vector3 GetLastNonZeroMoveVector3Normalized();
        string GetBindingText(KeybindType keybindType);
        void RebindBinding(KeybindType type, Action onActionRebound);

        public event Action OnInteractTry;
        public event Action OnPause;
    }
}