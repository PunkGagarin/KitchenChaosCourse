using System;
using UnityEngine;

public class CuttingProgressBarBurningAnimationUI : MonoBehaviour
{

    private Animator _animator;
    private static readonly int IsFlashing = Animator.StringToHash("IsFlashing");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TurnOnAnimation()
    {
        _animator.SetBool(IsFlashing, true);
    }

    public void TurnOffAnimation()
    {
        _animator.SetBool(IsFlashing, true);
    }
}