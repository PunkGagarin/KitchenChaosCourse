using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{

    public class KitchenGameManager : MonoBehaviour
    {

        private float _currentGameTimer;

        [SerializeField]
        private float _waitingStartTimer = 1f;

        [SerializeField]
        private float _gameTimerMax = 10f;

        private KitchenGameManagerState _state;

        [field: SerializeField]
        public float CountDownTimer { get; private set; } = 3f;

        public KitchenGameManagerState State
        {
            get => _state;

            private set
            {
                _state = value;
                OnStateChanged.Invoke(_state);
            }
        }

        public Action<KitchenGameManagerState> OnStateChanged = delegate { };

        public void Awake()
        {
            State = KitchenGameManagerState.WaitingToStart;
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

        private void PrepareToStart()
        {
            _waitingStartTimer -= Time.deltaTime;
            if (_waitingStartTimer < 0f)
            {
                State = KitchenGameManagerState.CountdownToStart;
            }
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

        public float GetGameTimerNormalized()
        {
            return 1 - (_currentGameTimer / _gameTimerMax);
        }
    }

}