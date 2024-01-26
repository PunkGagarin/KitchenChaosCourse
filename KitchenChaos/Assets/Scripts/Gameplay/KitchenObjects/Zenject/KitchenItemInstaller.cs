using Gameplay.KitchenObjects.Scriptables;
using UnityEngine;
using Zenject;

namespace Gameplay.KitchenObjects.Zenject
{

    public class KitchenItemInstaller : MonoInstaller
    {

        [SerializeField]
        private KitchenItemSoFactory _kitchenItemSoFactory;
        

        public override void InstallBindings()
        {
            Container.Bind<KitchenItemSoFactory>().FromNewScriptableObject(_kitchenItemSoFactory);
            // Container.Bind<KitchenItemSoFactory>().FromScriptableObjectResource("KitchenObjects/KitchenItemSoFactory.asset");
        }
    }

}