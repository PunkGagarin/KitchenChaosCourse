using System;
using Gameplay.Controllers;
using Gameplay.Counter;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio
{

    public class SoundManager : BaseAudioManager
    {
        private const string PLAYER_PREFS_NAME = "SoundEffectVolume";
        private const float DEFAULT_VOLUME = 1f;

        [Inject] private DeliveryManager _deliveryManager;
        [Inject] private PlayerKitchenItemHolder _playerKitchenItemHolder;

        [SerializeField]
        private SoundsFactorySO _soundsFactory;

        private void Awake()
        {
            SetPlayerPrefsName();
            Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_NAME, DEFAULT_VOLUME);
        }

        private void Start()
        {
            _deliveryManager.OnOrderCompleted += OnOrderCompletedHandle;
            _deliveryManager.OnOrderFailed += OnOrderFailedHandle;
            _playerKitchenItemHolder.OnSetKitchenItem += OnSetKitchenItemHandle;
        }


        private void OnDestroy()
        {
            _deliveryManager.OnOrderCompleted -= OnOrderCompletedHandle;
            _deliveryManager.OnOrderFailed -= OnOrderFailedHandle;
            _playerKitchenItemHolder.OnSetKitchenItem -= OnSetKitchenItemHandle;
        }


        private void OnOrderCompletedHandle(OrderCompletedEventArgs orderCompletedEventArgs)
        {
            var soundToPlay = GetRandomSoundByType(GameAudioType.OrderSuccess);
            PlaySound(soundToPlay, orderCompletedEventArgs.transform.position);
        }

        private AudioClip GetRandomSoundByType(GameAudioType type)
        {
            return _soundsFactory.GetRandomClipByType(type);
        }


        private void OnOrderFailedHandle(Transform transform)
        {
            var soundToPlay = GetRandomSoundByType(GameAudioType.OrderFail);
            PlaySound(soundToPlay, transform.position);
        }

        private void OnSetKitchenItemHandle(Transform transform)
        {
            var soundToPlay = GetRandomSoundByType(GameAudioType.ItemPickup);
            PlaySound(soundToPlay, transform.position);
        }

        public void PlayRandomSoundByType(GameAudioType type, Transform transform, float volumeMultiplier)
        {
            var soundToPlay = GetRandomSoundByType(type);
            PlaySound(soundToPlay, transform.position, volumeMultiplier);
        }

        public void PlayRandomSoundByType(GameAudioType type, Transform transform)
        {
            var soundToPlay = GetRandomSoundByType(type);
            PlaySound(soundToPlay, transform.position);
        }

        public void PlayRandomSoundByType(GameAudioType type, Vector3 position)
        {
            var soundToPlay = GetRandomSoundByType(type);
            PlaySound(soundToPlay, position);
        }

        public void PlaySoundByType(GameAudioType type,int soundIndex,  Vector3 position)
        {
            var soundToPlay = _soundsFactory.GetClipByTypeAndIndex(type, soundIndex);
            PlaySound(soundToPlay, position);
        }

        private void PlaySound(AudioClip clip, Vector3 position, float volumeMultiplier = DEFAULT_VOLUME)
        {
            AudioSource.PlayClipAtPoint(clip, position, volumeMultiplier * Volume);
        }

        protected override void SetPlayerPrefsName()
        {
            _playerPrefsName = PLAYER_PREFS_NAME;
        }
    }

}