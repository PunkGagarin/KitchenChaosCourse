using Gameplay;
using Gameplay.KitchenObjects;
using Gameplay.Player;
using Gameplay.PLayer;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerInstaller : MonoInstaller
{

    [FormerlySerializedAs("_gameInput")] [SerializeField]
    private GameInputController _gameInputController;

    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private PlayerInteractions _playerInteractions;

    [SerializeField]
    private PlayerKitchenItemHolder _playerKitchenItemHolder;

    public override void InstallBindings()
    {

        Container.BindInterfacesAndSelfTo<KitchenItemSpawner>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<GameInputController>()
            .FromInstance(_gameInputController)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerInteractions>()
            .FromInstance(_playerInteractions)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerMovement>()
            .FromInstance(_playerMovement)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerKitchenItemHolder>()
            .FromInstance(_playerKitchenItemHolder)
            .AsSingle();
    }
}