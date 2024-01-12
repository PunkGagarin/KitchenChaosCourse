using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{


    private PlayerMovement _playerMovement;
    private Animator _animator;

    private static readonly int IsMoving = Animator.StringToHash("IsWalking");


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponentInChildren<Animator>();
        
        _playerMovement.OnStartWalking += OnStartWalkingHandle;
        _playerMovement.OnStopWalking += OnStopWalkingHandle;
    }

    private void OnDestroy()
    {
        _playerMovement.OnStartWalking -= OnStartWalkingHandle;
        _playerMovement.OnStopWalking -= OnStopWalkingHandle;
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