using UnityEngine;
using Zenject;

namespace Gameplay.Zenject
{

    public class GameManagerInstallers : MonoInstaller
    {

        [SerializeField]
        private KitchenGameManager _kitchenGameManager;

        public override void InstallBindings()
        {
            Container.Bind<KitchenGameManager>()
                .FromInstance(_kitchenGameManager)
                .AsSingle();
        }
    }

}