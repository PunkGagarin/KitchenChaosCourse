using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{

    public class PlayerInteractions : MonoBehaviour
    {
        private Vector3 _interactionDirection;

        [SerializeField]
        private LayerMask _counterLayermask;

        [SerializeField]
        private float _interactionDistance;

        [Inject] private GameInput _gameInput;

        private void Awake()
        {
            _gameInput.OnInteractTry += CheckInteraction;
        }

        private void OnDestroy()
        {
            _gameInput.OnInteractTry -= CheckInteraction;
        }

        private void CheckInteraction()
        {
            Vector3 direction = _gameInput.GetLastNonZeroMoveVector3Normalized();

            if (direction != Vector3.zero)
            {
                _interactionDirection = direction;
            }
            
            if (Physics.Raycast(transform.position, _interactionDirection, out RaycastHit raycastHit,
                    _interactionDistance, _counterLayermask))
            {
                if (raycastHit.transform.TryGetComponent<EmptyCounter>(out var component))
                {
                    component.Interact();
                }
            }
        }
    }

}