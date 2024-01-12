using System;
using UnityEngine;
using Zenject;

namespace Gameplay.PLayer
{

    public class PlayerMovement : MonoBehaviour
    {

        private Vector3 _moveDir = Vector3.zero;

        private bool _isWalking;

        [SerializeField]
        private float _speedMultiplier = 5f;

        [SerializeField]
        private float _rotateSpeed = 10f;

        [SerializeField]
        private float _playerHeight = 2f;

        [SerializeField]
        private float _raycastRadius = .6f;

        [Inject] private GameInput _gameInput;

        public Action OnStartWalking = delegate { };
        public Action OnStopWalking = delegate { };

        public void Update()
        {
            _moveDir = Vector3.zero;

            var inputVector = _gameInput.GetVector2InputNormalized();

            _moveDir.x = inputVector.x;
            _moveDir.z = inputVector.y;

            if (inputVector != Vector2.zero)
            {
                float speedMultiplier = _speedMultiplier * Time.deltaTime;

                bool noObstacleInFront = CheckObjectInFront(_moveDir, speedMultiplier);

                if (!noObstacleInFront)
                {
                    TryFindOtherDirectionsToMove(ref _moveDir, speedMultiplier);
                }

                if (noObstacleInFront)
                {
                    Move(speedMultiplier);
                }
                Rotate();
            }
            else
            {
                TryStopMoving();
            }
        }

        private void TryFindOtherDirectionsToMove(ref Vector3 moveDir, float speedMultiplier)
        {
            Vector3 moveDirX = new Vector3(_moveDir.x, 0, 0).normalized;
            bool noObstacleInFront = CheckObjectInFront(moveDirX, speedMultiplier);
            if (noObstacleInFront)
            {
                moveDir = moveDirX;
            }

            Vector3 moveDirZ = new Vector3(0, 0, _moveDir.z).normalized;
            noObstacleInFront = CheckObjectInFront(moveDirZ, speedMultiplier);
            if (noObstacleInFront)
            {
                moveDir = moveDirZ;
            }
        }

        private bool CheckObjectInFront(Vector3 _moveDirX, float speedMultiplier)
        {
            return !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * _playerHeight, _raycastRadius, _moveDirX, speedMultiplier);
        }

        private void Move(float speedMultiplier)
        {
            if (!_isWalking)
            {
                _isWalking = true;
                OnStartWalking.Invoke();
            }

            transform.position += _moveDir * speedMultiplier;
        }

        private void TryStopMoving()
        {
            if (_isWalking)
            {
                _isWalking = false;
                OnStopWalking.Invoke();
            }
        }

        private void Rotate()
        {
            //Changing blue axis of the object. Because object is rotated in 180 degrees, i use minus moveDir (-moveDir)
            transform.forward = Vector3.Slerp(transform.forward, -_moveDir, _rotateSpeed * Time.deltaTime);
        }
    }

}