using Gameplay.Controllers;
using Gameplay.Counter;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio
{

    public class SoundManager : MonoBehaviour
    {
        private const float DEFAULT_VOLUME = 1f;

        [Inject] private DeliveryManager _deliveryManager;
        [Inject] private PlayerKitchenItemHolder _playerKitchenItemHolder;

        [SerializeField]
        private SoundsFactorySO _soundsFactory;

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

        public void PlayRandomSoundByType(GameAudioType itemDrop, Transform transform, float volume)
        {
            var soundToPlay = GetRandomSoundByType(itemDrop);
            PlaySound(soundToPlay, transform.position, volume);
        }

        public void PlayRandomSoundByType(GameAudioType itemDrop, Transform transform)
        {
            var soundToPlay = GetRandomSoundByType(itemDrop);
            PlaySound(soundToPlay, transform.position);
        }

        private void PlaySound(AudioClip clip, Vector3 position, float volume = DEFAULT_VOLUME)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }

}