using Gameplay.Controllers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Gameplay.KitchenObjects.Zenject
{

    public class KitchenItemInstaller : MonoInstaller
    {

        [FormerlySerializedAs("_deliveryController")] [SerializeField]
        private DeliveryManager _deliveryManager;
        

        public override void InstallBindings()
        {
            Container.Bind<DeliveryManager>().FromInstance(_deliveryManager);
        }
    }

}