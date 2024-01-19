﻿using Gameplay.Counter;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{

    public class PlayerInteractions : MonoBehaviour
    {
        private Vector3 _interactionDirection;

        private BaseCounter _selectedCounter;

        private IKitchenItemParent _playerKitchenItemHolder;

        [SerializeField]
        private LayerMask _counterLayermask;

        [SerializeField]
        private float _interactionDistance;

        [Inject] private GameInput _gameInput;

        private void Awake()
        {
            _gameInput.OnInteractTry += OnInteractInputHandle;
            _gameInput.OnInteractAlternativeTry += OnInteractAlternativeInputHandle;
            _playerKitchenItemHolder = GetComponent<PlayerKitchenItemHolder>();
        }

        private void OnDestroy()
        {
            _gameInput.OnInteractTry -= OnInteractInputHandle;
            _gameInput.OnInteractAlternativeTry -= OnInteractAlternativeInputHandle;
        }

        private void OnInteractInputHandle()
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.Interact(_playerKitchenItemHolder);
            }
        }

        private void OnInteractAlternativeInputHandle()
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.InteractAlternative(_playerKitchenItemHolder);
            }
        }

        private void FixedUpdate()
        {
            SelectCounterWithRaycast();
        }

        private void SelectCounterWithRaycast()
        {
            var newSelectedCounter = CheckCounterWithRaycast();

            if (_selectedCounter != newSelectedCounter)
            {
                if (_selectedCounter != null)
                    _selectedCounter.TurnOffSelectedVisual();
                
                if (newSelectedCounter != null)
                    newSelectedCounter.TurnOnSelectedVisual();

                _selectedCounter = newSelectedCounter;
            }
        }

        private BaseCounter CheckCounterWithRaycast()
        {
            BaseCounter raycastedCounter = null;
            Vector3 direction = _gameInput.GetLastNonZeroMoveVector3Normalized();

            if (direction != Vector3.zero)
            {
                _interactionDirection = direction;
            }

            if (Physics.Raycast(transform.position, _interactionDirection, out RaycastHit raycastHit,
                    _interactionDistance, _counterLayermask))
            {
                if (raycastHit.transform.TryGetComponent<BaseCounter>(out var component))
                {
                    raycastedCounter = component;
                }
            }
            return raycastedCounter;
        }
    }

}