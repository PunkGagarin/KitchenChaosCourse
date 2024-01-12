using UnityEngine;

namespace Gameplay
{

    public class GameInput : MonoBehaviour
    {
        private PlayerInputAction _inputAction;

        private void Awake()
        {
            _inputAction = new PlayerInputAction();
            _inputAction.Player.Enable();
        }

        public Vector2 GetVector2InputNormalized()
        {
            var v2Input = _inputAction.Player.Move.ReadValue<Vector2>();
            return v2Input.normalized;
        }
    }

}