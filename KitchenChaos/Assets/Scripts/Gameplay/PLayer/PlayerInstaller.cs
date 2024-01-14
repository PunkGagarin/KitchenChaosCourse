using Gameplay;
using Gameplay.Player;
using Gameplay.PLayer;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{

    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private GameInput _gameInput;

    [SerializeField]
    private PlayerInteractions _playerInteractions;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameInput>()
            .FromInstance(_gameInput)
            .AsSingle();
        
        Container.BindInterfacesAndSelfTo<PlayerInteractions>()
            .FromInstance(_playerInteractions)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerMovement>()
            .FromInstance(_playerMovement)
            .AsSingle();
    }
}