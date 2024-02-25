using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{

    public class GameInputManager : MonoBehaviour
    {
        private const string PLAYER_PREFS_PLAYER_BINDING = "PlayerBinding";
        private PlayerInputAction _inputAction;
        private Vector3 _lastNonZeroMoveInput;

        public Action OnInteractTry = delegate { };
        public Action OnInteractAlternativeTry = delegate { };
        public Action OnPause = delegate { };
        public Action OnKeybindRebind = delegate { };

        private void Awake()
        {
            _inputAction = new PlayerInputAction();
            
            if (PlayerPrefs.HasKey(PLAYER_PREFS_PLAYER_BINDING))
                _inputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_PLAYER_BINDING));
            
            _inputAction.Player.Enable();
            _inputAction.Player.Interact.performed += InteractHandle;
            _inputAction.Player.InteractAlternative.performed += InteractAlternativeHandle;
            _inputAction.Player.Pause.performed += PauseHandle;

        }

        private void OnDestroy()
        {
            _inputAction.Dispose();
        }

        private void InteractHandle(InputAction.CallbackContext obj)
        {
            OnInteractTry.Invoke();
        }

        private void InteractAlternativeHandle(InputAction.CallbackContext obj)
        {
            OnInteractAlternativeTry.Invoke();
        }

        private void PauseHandle(InputAction.CallbackContext obj)
        {
            OnPause.Invoke();
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

        private Vector2 GetVector2InputNormalized()
        {
            var v2Input = _inputAction.Player.Move.ReadValue<Vector2>();
            return v2Input.normalized;
        }

        public Vector3 GetLastNonZeroMoveVector3Normalized()
        {
            return _lastNonZeroMoveInput;
        }

        public string GetBindingText(KeybindType keybindType)
        {
            switch (keybindType)
            {
                default:
                case KeybindType.Interact:
                    return _inputAction.Player.Interact.bindings[0].ToDisplayString();
                case KeybindType.InteractAlternate:
                    return _inputAction.Player.InteractAlternative.bindings[0].ToDisplayString();
                case KeybindType.Pause:
                    return _inputAction.Player.Pause.bindings[0].ToDisplayString();
                case KeybindType.MoveUp:
                    return _inputAction.Player.Move.bindings[1].ToDisplayString();
                case KeybindType.MoveDown:
                    return _inputAction.Player.Move.bindings[2].ToDisplayString();
                case KeybindType.MoveLeft:
                    return _inputAction.Player.Move.bindings[3].ToDisplayString();
                case KeybindType.MoveRight:
                    return _inputAction.Player.Move.bindings[4].ToDisplayString();
            }
        }

        public void RebindBinding(KeybindType type, Action onActionRebound)
        {
            _inputAction.Disable();

            InputAction action;
            int actionIndex;

            switch (type)
            {
                default:
                case KeybindType.Interact:
                    action = _inputAction.Player.Interact;
                    actionIndex = 0;
                    break;
                case KeybindType.InteractAlternate:
                    action = _inputAction.Player.InteractAlternative;
                    actionIndex = 0;
                    break;
                case KeybindType.Pause:
                    action = _inputAction.Player.Pause;
                    actionIndex = 0;
                    break;
                case KeybindType.MoveUp:
                    action = _inputAction.Player.Move;
                    actionIndex = 1;
                    break;
                case KeybindType.MoveDown:
                    action = _inputAction.Player.Move;
                    actionIndex = 2;
                    break;
                case KeybindType.MoveLeft:
                    action = _inputAction.Player.Move;
                    actionIndex = 3;
                    break;
                case KeybindType.MoveRight:
                    action = _inputAction.Player.Move;
                    actionIndex = 4;
                    break;
            }


            action.PerformInteractiveRebinding(actionIndex)
                .OnComplete(rebindOperation => OnRebindingCompleteHandle(rebindOperation, onActionRebound))
                .Start();
        }

        private void OnRebindingCompleteHandle(InputActionRebindingExtensions.RebindingOperation rebindOperation,
            Action onActionRebound)
        {
            rebindOperation.Dispose();
            _inputAction.Enable();
            
            PlayerPrefs.SetString(PLAYER_PREFS_PLAYER_BINDING, _inputAction.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            OnKeybindRebind?.Invoke();
            onActionRebound.Invoke();
        }
    }

}