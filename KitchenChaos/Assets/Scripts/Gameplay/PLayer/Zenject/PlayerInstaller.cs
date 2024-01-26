using Gameplay;
using Gameplay.KitchenObjects;
using Gameplay.Player;
using Gameplay.PLayer;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{

    [SerializeField]
    private GameInput _gameInput;

    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private PlayerInteractions _playerInteractions;

    [SerializeField]
    private PlayerKitchenItemHolder _playerKitchenItemHolder;

    public override void InstallBindings()
    {

        Container.BindInterfacesAndSelfTo<KitchenItemSpawner>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<GameInput>()
            .FromInstance(_gameInput)
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