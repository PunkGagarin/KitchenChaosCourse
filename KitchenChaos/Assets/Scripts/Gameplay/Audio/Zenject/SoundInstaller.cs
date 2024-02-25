using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Zenject
{

    public class SoundInstaller : MonoInstaller
    {

        [SerializeField]
        private SoundManager _soundManager;

        [SerializeField]
        private MusicManager _musicManager;

        public override void InstallBindings()
        {
            Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();
            Container.Bind<MusicManager>().FromInstance(_musicManager).AsSingle();
        }
    }

}