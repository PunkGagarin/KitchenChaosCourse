using System;
using Gameplay.Audio;
using UnityEngine;

public class MusicManager : BaseAudioManager
{

    private const string PLAYER_PREFS_NAME = "MusicVolume";
    private const float DEFAULT_VOLUME = .3f;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        SetPlayerPrefsName();
        
        Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_NAME, DEFAULT_VOLUME);
        _audioSource.volume = Volume;
    }


    public override void ChangeVolume()
    {
        base.ChangeVolume();
        _audioSource.volume = Volume;
    }

    protected override void SetPlayerPrefsName()
    {
        _playerPrefsName = PLAYER_PREFS_NAME;
    }


}