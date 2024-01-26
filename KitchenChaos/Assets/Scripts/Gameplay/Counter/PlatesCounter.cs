using System;
using Gameplay.Counter.Visual;
using Gameplay.KitchenObjects;
using UnityEngine;
using Zenject;

namespace Gameplay.Counter
{

    public class PlatesCounter : BaseCounter
    {

        private float _currentSpawnTimer;

        private int _spawnedPlateAmount;

        private PlatesCounterVisual _visual;

        [Inject] private KitchenItemSpawner _spawner;

        [SerializeField]
        private KitchenItemSO _kitchenItemSo;

        [SerializeField]
        private float _defaultSpawnTimer;

        [SerializeField]
        private float _maxPlateAmount;

        protected override void Awake()
        {
            base.Awake();
            _visual = GetComponentInChildren<PlatesCounterVisual>();
        }

        private void Update()
        {
            _currentSpawnTimer += Time.deltaTime;
            if (_currentSpawnTimer >= _defaultSpawnTimer)
            {
                _currentSpawnTimer = 0f;
                if (_spawnedPlateAmount < _maxPlateAmount)
                {
                    _spawnedPlateAmount++;
                    _visual.SpawnNewPlate();
                }
            }
        }

        public override void Interact(IKitchenItemParent player)
        {
            if (!player.HasKitchenItem())
            {
                if (_spawnedPlateAmount > 0)
                {
                    _spawnedPlateAmount--;
                    _currentSpawnTimer = 0f;
                    var plate = _spawner.SpawnKitchenItem(_kitchenItemSo.Prefab, _onTopSpawnPoint);
                    player.SetKitchenItem(plate);

                    _visual.RemoveOnePlate();
                }
            }
        }
    }

}