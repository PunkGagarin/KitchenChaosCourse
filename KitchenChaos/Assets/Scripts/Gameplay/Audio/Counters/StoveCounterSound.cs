using System;
using Gameplay.Counter;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Counters
{

    public class StoveCounterSound : MonoBehaviour
    {

        private AudioSource _audioSource;

        private float Timer;
        private bool isPlayingWarningSound = false;

        [Inject] private SoundManager _soundManager;

        [SerializeField]
        private StoveCounter _stove;


        private void Start()
        {
            _stove.OnStateChanged += CheckForSoundPlay;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!isPlayingWarningSound) return;

            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                Timer = .2f;
                _soundManager.PlaySoundByType(GameAudioType.Warning, 0, transform.position);
            }
        }

        private void CheckForSoundPlay(StoveState newState)
        {
            bool needToPlaySound = newState is StoveState.Frying or StoveState.Fried;
            if (needToPlaySound)
                _audioSource.Play();
            else
                _audioSource.Stop();
        }

        public void PlayWarningSound()
        {
            isPlayingWarningSound = true;
        }

        public void StopPlayingWarningSound()
        {
            isPlayingWarningSound = false;
        }

    }

}