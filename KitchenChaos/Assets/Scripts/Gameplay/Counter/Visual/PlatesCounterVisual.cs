using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Counter.Visual
{

    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField]
        private float _distanceBetweenPlates = 0.11f;

        [SerializeField]
        private Transform _platesPrefab;

        [SerializeField]
        private Transform _onTopSpawnPoint;


        private List<Transform> _spawnedPlates = new();

        public void SpawnNewPlate()
        {
            Transform newPlate = Instantiate(_platesPrefab, _onTopSpawnPoint);

            newPlate.localPosition = new Vector3(0, _spawnedPlates.Count * _distanceBetweenPlates, 0);
            _spawnedPlates.Add(newPlate);
        }

        public void RemoveOnePlate()
        {
            Transform lastPlate = _spawnedPlates[^1];
            
            //todo: make a pool of ojects instead
            _spawnedPlates.Remove(lastPlate);
            Destroy(lastPlate.gameObject);
        }
    }

}