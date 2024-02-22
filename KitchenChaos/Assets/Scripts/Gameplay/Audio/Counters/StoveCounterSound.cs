using Gameplay.Counter;
using UnityEngine;

namespace Gameplay.Audio.Counters
{

    public class StoveCounterSound : MonoBehaviour
    {

        private AudioSource _audioSource;

        [SerializeField]
        private StoveCounter _stove;


        private void Start()
        {
            _stove.OnStateChanged += CheckForSoundPlay;
            _audioSource = GetComponent<AudioSource>();
        }

        private void CheckForSoundPlay(StoveState newState)
        {
            bool needToPlaySound = newState is StoveState.Frying or StoveState.Fried;
            if (needToPlaySound)
                _audioSource.Play();
            else
                _audioSource.Stop();
        }

    }

}