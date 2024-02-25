using System;
using DefaultNamespace;
using Gameplay.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI.Options
{

    public class OptionsUI : BaseContentUI
    {
        private ControlBindingUI _controlBindingUI;

        private Action OnCloseAction;

        [Inject] private SoundManager _soundManager;
        [Inject] private MusicManager _musicManager;

        [SerializeField]
        private Button _soundEffectsButton;

        [SerializeField]
        private Button _musicButton;

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private TextMeshProUGUI _soundEffectsText;

        [SerializeField]
        private TextMeshProUGUI _musicText;

        private void Awake()
        {
            _soundEffectsButton.onClick.AddListener(OnSoundEffectsClickHandle);
            _musicButton.onClick.AddListener(OnMusicClickHandle);
            _closeButton.onClick.AddListener(Hide);
            _controlBindingUI = GetComponentInChildren<ControlBindingUI>();
        }

        private void Start()
        {
            UpdateSoundsVisual();
            UpdateMusicVisual();
            // UpdateKeyBindVisual();
            Hide();
        }

        private void OnDestroy()
        {
            _soundEffectsButton.onClick.RemoveListener(_soundManager.ChangeVolume);
            _musicButton.onClick.RemoveListener(OnMusicClickHandle);
            _closeButton.onClick.RemoveListener(Hide);
        }

        private void OnSoundEffectsClickHandle()
        {
            _soundManager.ChangeVolume();
            UpdateSoundsVisual();
        }

        private void UpdateSoundsVisual()
        {
            _soundEffectsText.text = "Sound Effects: " + Math.Round(_soundManager.Volume * 10f);
        }

        private void OnMusicClickHandle()
        {
            _musicManager.ChangeVolume();
            UpdateMusicVisual();
        }

        private void UpdateMusicVisual()
        {
            _musicText.text = "Music: " + Math.Round(_musicManager.Volume * 10f);
        }

        private void UpdateKeyBindVisual()
        {
            _controlBindingUI.UpdateKeyBindVisual();
        }

        public override void Show()
        {
            base.Show();
            _soundEffectsButton.Select();
        }

        public override void Hide()
        {
            base.Hide();
            OnCloseAction?.Invoke();
        }

        public void Show(Action onCloseAction)
        {
            OnCloseAction = onCloseAction;
            Show();
        }
    }

}