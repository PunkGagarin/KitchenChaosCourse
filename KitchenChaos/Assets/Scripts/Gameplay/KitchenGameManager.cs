using System;
using UnityEngine;
using Zenject;

namespace Gameplay
{

    public class KitchenGameManager : MonoBehaviour
    {

        private bool _isGamePaused;
        private float _currentGameTimer;


        [SerializeField]
        private float _gameTimerMax = 10f;

        private KitchenGameManagerState _state;

        [Inject] private IGameInputManager _gameInput;

        [field: SerializeField]
        public float CountDownTimer { get; private set; } = 3f;

        private KitchenGameManagerState State
        {
            get => _state;

            set
            {
                _state = value;
                OnStateChanged.Invoke(_state);
            }
        }

        public void SetInputManager(IGameInputManager inputManager)
        {
            _gameInput = inputManager;
        }

        public Action<KitchenGameManagerState> OnStateChanged = delegate { };
        public Action OnGamePaused = delegate { };
        public Action OnGameUnpaused = delegate { };

        public void Awake()
        {
            State = KitchenGameManagerState.WaitingToStart;
        }

        private void Start()
        {
            _gameInput.OnPause += ToggleGamePause;
            _gameInput.OnInteractTry += OnInteractHandle;
        }

        private void Update()
        {
            switch (State)
            {
                case KitchenGameManagerState.WaitingToStart:
                    PrepareToStart();
                    break;
                case KitchenGameManagerState.CountdownToStart:
                    CountdownBeforeStart();
                    break;
                case KitchenGameManagerState.GamePlaying:
                    CountGameplayTimer();
                    break;
                case KitchenGameManagerState.GameOver:
                    break;
            }
        }

        private void OnInteractHandle()
        {
            if (_state == KitchenGameManagerState.WaitingToStart)
                State = KitchenGameManagerState.CountdownToStart;
        }

        private void PrepareToStart()
        {
        }

        private void CountdownBeforeStart()
        {
            CountDownTimer -= Time.deltaTime;
            if (CountDownTimer < 0f)
            {
                State = KitchenGameManagerState.GamePlaying;
                _currentGameTimer = _gameTimerMax;
            }
        }

        private void CountGameplayTimer()
        {
            _currentGameTimer -= Time.deltaTime;
            if (_currentGameTimer < 0f)
            {
                State = KitchenGameManagerState.GameOver;
            }
        }

        public bool IsGamePlaying()
        {
            return State == KitchenGameManagerState.GamePlaying;
        }

        public KitchenGameManagerState GetCurrentState()
        {
            return State;
        }

        public float GetGameTimerNormalized()
        {
            return 1 - (_currentGameTimer / _gameTimerMax);
        }

        public void ToggleGamePause()
        {
            if (_isGamePaused)
            {
                Time.timeScale = 1f;
                _isGamePaused = false;
                OnGameUnpaused.Invoke();
            }
            else
            {
                Time.timeScale = 0f;
                _isGamePaused = true;
                OnGamePaused.Invoke();
            }
        }
    }

}