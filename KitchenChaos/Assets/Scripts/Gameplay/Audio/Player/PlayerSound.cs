using Gameplay.Audio;
using Gameplay.PLayer;
using UnityEngine;
using Zenject;

public class PlayerSound : MonoBehaviour
{
    private float _currentTimer;

    private PlayerMovement _playerMovement;

    [Inject] private SoundManager _soundManager;

    [SerializeField]
    private float _footStepTimer = 0.1f;

    [SerializeField]
    private float stepVolume = 3f;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!_playerMovement.IsMoving()) return;

        _currentTimer -= Time.deltaTime;
        if (_currentTimer <= 0)
        {
            _currentTimer = _footStepTimer;
            _soundManager.PlayRandomSoundByType(GameAudioType.Footstep, transform, stepVolume);
        }
    }
}