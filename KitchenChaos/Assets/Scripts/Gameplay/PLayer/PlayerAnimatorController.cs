using Gameplay.PLayer;
using UnityEngine;

namespace Gameplay.Player
{

    public class PlayerAnimatorController : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private Animator _animator;

        private static readonly int IsMoving = Animator.StringToHash("IsWalking");


        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();

            _playerMovement.OnStartMoving += OnStartWalkingHandle;
            _playerMovement.OnStopMoving += OnStopWalkingHandle;
        }

        private void OnDestroy()
        {
            _playerMovement.OnStartMoving -= OnStartWalkingHandle;
            _playerMovement.OnStopMoving -= OnStopWalkingHandle;
        }

        private void OnStartWalkingHandle()
        {
            _animator.SetBool(IsMoving, true);
        }

        private void OnStopWalkingHandle()
        {
            _animator.SetBool(IsMoving, false);
        }
    }

}