using UnityEngine;

namespace Gameplay.Counter
{

    public class CuttingCounterAnimation : MonoBehaviour
    {
        private static readonly int Cut = Animator.StringToHash("Cut");

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayOpenAnimation()
        {
            _animator.SetTrigger(Cut);
        }


    }

}