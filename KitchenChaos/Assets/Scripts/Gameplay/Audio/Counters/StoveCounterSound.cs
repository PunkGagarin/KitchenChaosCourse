using System.Reflection;
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

        [Inject] private ISoundManager _soundManager;

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

            //todo: Stub method to show that we have problem with those methods.
            //todo: SOLVE ME?????
            //как написать тест на этот метод не меняя класс??
            //StoveCounterSoundTest
            _stove.OnTestMethod();
            
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

    public interface ISoundManager
    {
        void PlaySoundByType(GameAudioType type, int soundIndex, Vector3 transformPosition);
    }
}