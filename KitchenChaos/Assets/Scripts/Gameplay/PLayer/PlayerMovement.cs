using System;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.PLayer
{

    public class PlayerMovement : MonoBehaviour
    {

        private bool _isMoving;

        [SerializeField]
        private float _speedMultiplier = 5f;

        [SerializeField]
        private float _rotateSpeed = 10f;

        [SerializeField]
        private float _playerHeight = 2f;

        [SerializeField]
        private float _raycastRadius = .6f;

        [Inject] private GameInputManager _gameInputManager;
        [Inject] private PlayerInteractions _playerInteractions;

        public Action OnStartMoving = delegate { };
        public Action OnStopMoving = delegate { };

        public void Update()
        {
            var moveDir = _gameInputManager.GetVector3InputNormalized();
            HandleMovement(moveDir);
        }

        private void HandleMovement(Vector3 moveDir)
        {
            if (moveDir != Vector3.zero)
            {
                float distanceToMove = _speedMultiplier * Time.deltaTime;

                bool freeToMove = NoObjectInFront(moveDir, distanceToMove);

                if (!freeToMove)
                {
                    freeToMove = TryFindOtherDirectionsToMove(ref moveDir, distanceToMove);
                }

                if (freeToMove)
                {
                    Move(distanceToMove, moveDir);
                }
                Rotate(moveDir);
            }
            else
            {
                TryStopMoving();
            }
        }

        private bool TryFindOtherDirectionsToMove(ref Vector3 moveDir, float speedMultiplier)
        {
            bool isFreeToMove = false;
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            if (moveDir.x != 0 && NoObjectInFront(moveDirX, speedMultiplier))
            {
                moveDir = moveDirX;
                isFreeToMove = true;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                if (moveDir.z != 0 && NoObjectInFront(moveDirZ, speedMultiplier))
                {
                    moveDir = moveDirZ;
                    isFreeToMove = true;
                }
            }


            return isFreeToMove;
        }

        private bool NoObjectInFront(Vector3 moveDir, float speedMultiplier)
        {
            return !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * _playerHeight, _raycastRadius, moveDir, speedMultiplier);
        }

        private void Move(float speedMultiplier, Vector3 moveDir)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                OnStartMoving.Invoke();
            }

            transform.position += moveDir * speedMultiplier;
        }

        private void TryStopMoving()
        {
            if (_isMoving)
            {
                _isMoving = false;
                OnStopMoving.Invoke();
            }
        }

        private void Rotate(Vector3 moveDir)
        {
            //Changing blue axis of the object. Because object is rotated in 180 degrees, i use minus moveDir (-moveDir)
            transform.forward = Vector3.Slerp(transform.forward, -moveDir, _rotateSpeed * Time.deltaTime);
        }

        public bool IsMoving()
        {
            return _isMoving;
        }
    }

}