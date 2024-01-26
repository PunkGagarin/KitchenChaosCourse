using UnityEngine;

namespace Gameplay.Counter
{

    public class ContainerCounterAnimation : MonoBehaviour
    {
        private static readonly int OpenClose = Animator.StringToHash("OpenClose");

        private Animator _animator;

        //todo: не совсем согласен, я бы управлял этой анимацией (хранил тут, но управлял) из ContainerCounter, а не наоборот.
        [SerializeField]
        private ContainerCounter _containerCounter;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _containerCounter.OnKitchenItemSpawn += PlayOpenAnimation;
        }

        private void PlayOpenAnimation()
        {
            _animator.SetTrigger(OpenClose);
        }


    }

}