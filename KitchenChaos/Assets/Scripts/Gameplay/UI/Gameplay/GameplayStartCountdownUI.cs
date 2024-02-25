using Gameplay.Audio;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.Gameplay
{

    public class GameplayStartCountdownUI : MonoBehaviour
    {
        private static readonly int NumberPopup = Animator.StringToHash("NumberPopup");


        private int _currentCountdownNumber;
        private Animator _animator;

        [Inject] private KitchenGameManager _kitchenGameManager;
        [Inject] private SoundManager _soundManager;

        [SerializeField]
        private TextMeshProUGUI _countdownNumberText;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _kitchenGameManager.OnStateChanged += OnStateChangedHandle;
            Hide();
        }

        private void Update()
        {
            if (_countdownNumberText.gameObject.activeSelf)
            {
                int countdownNumber = Mathf.CeilToInt(_kitchenGameManager.CountDownTimer);
                _countdownNumberText.text = countdownNumber.ToString();

                if (_currentCountdownNumber != countdownNumber)
                {
                    _currentCountdownNumber = countdownNumber;
                    _animator.SetTrigger(NumberPopup);
                    _soundManager.PlaySoundByType(GameAudioType.Warning, 1, Vector3.zero);
                }
            }
        }

        private void OnStateChangedHandle(KitchenGameManagerState newState)
        {
            if (newState == KitchenGameManagerState.CountdownToStart)
                Show();
            else
                Hide();
        }

        private void Show()
        {
            _countdownNumberText.gameObject.SetActive(true);
        }

        private void Hide()
        {
            //todo: брать текст из ГеймМанагера
            _countdownNumberText.text = 3.ToString();
            _countdownNumberText.gameObject.SetActive(false);
        }
    }

}