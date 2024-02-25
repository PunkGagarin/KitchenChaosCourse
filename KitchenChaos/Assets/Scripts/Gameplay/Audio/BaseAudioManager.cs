using UnityEngine;

namespace Gameplay.Audio
{

    public abstract class BaseAudioManager : MonoBehaviour
    {

        protected string _playerPrefsName;

        public float Volume { get; protected set; }

        public virtual void ChangeVolume()
        {
            Volume += .1f;
            if (Volume > 1f)
                Volume = 0f;

            PlayerPrefs.SetFloat(_playerPrefsName, Volume);
            PlayerPrefs.Save();
        }

        protected abstract void SetPlayerPrefsName();
    }

}