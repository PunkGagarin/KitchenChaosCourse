using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{

    public class GameInput : MonoBehaviour
    {
        private PlayerInputAction _inputAction;
        private Vector3 _lastNonZeroMoveInput;

        public Action OnInteractTry = delegate { };
        public Action OnInteractAlternativeTry = delegate { };

        private void Awake()
        {
            _inputAction = new PlayerInputAction();
            _inputAction.Player.Enable();
            _inputAction.Player.Interact.performed += InteractHandle;
            _inputAction.Player.InteractAlternative.performed += InteractAlternativeHandle;
        }

        private void InteractHandle(InputAction.CallbackContext obj)
        {
            OnInteractTry.Invoke();
        }

        private void InteractAlternativeHandle(InputAction.CallbackContext obj)
        {
            OnInteractAlternativeTry.Invoke();
        }

        public Vector2 GetVector2InputNormalized()
        {
            var v2Input = _inputAction.Player.Move.ReadValue<Vector2>();
            return v2Input.normalized;
        }

        public Vector3 GetVector3InputNormalized()
        {
            Vector3 moveDir = Vector3.zero;
            var inputVector = GetVector2InputNormalized();

            moveDir.x = inputVector.x;
            moveDir.z = inputVector.y;
            moveDir = moveDir.normalized;

            if (moveDir != Vector3.zero)
            {
                _lastNonZeroMoveInput = moveDir;
            }
            return moveDir;
        }

        public Vector3 GetLastNonZeroMoveVector3Normalized()
        {
            return _lastNonZeroMoveInput;
        }
    }

}