using System;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.PLayer
{

    public class PlayerMovement : MonoBehaviour
    {

        private bool _isWalking;

        [SerializeField]
        private float _speedMultiplier = 5f;

        [SerializeField]
        private float _rotateSpeed = 10f;

        [SerializeField]
        private float _playerHeight = 2f;

        [SerializeField]
        private float _raycastRadius = .6f;

        [Inject] private GameInputController _gameInputController;
        [Inject] private PlayerInteractions _playerInteractions;

        public Action OnStartWalking = delegate { };
        public Action OnStopWalking = delegate { };

        public void Update()
        {
            var moveDir = _gameInputController.GetVector3InputNormalized();
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
            if (!_isWalking)
            {
                _isWalking = true;
                OnStartWalking.Invoke();
            }

            transform.position += moveDir * speedMultiplier;
        }

        private void TryStopMoving()
        {
            if (_isWalking)
            {
                _isWalking = false;
                OnStopWalking.Invoke();
            }
        }

        private void Rotate(Vector3 moveDir)
        {
            //Changing blue axis of the object. Because object is rotated in 180 degrees, i use minus moveDir (-moveDir)
            transform.forward = Vector3.Slerp(transform.forward, -moveDir, _rotateSpeed * Time.deltaTime);
        }
    }

}