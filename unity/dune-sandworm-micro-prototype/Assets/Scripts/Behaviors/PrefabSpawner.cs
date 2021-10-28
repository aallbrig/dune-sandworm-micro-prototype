using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviors
{
    public class PrefabSpawner : MonoBehaviour, ICanSpawn
    {
        public event Action<GameObject> PrefabSpawned;

        [SerializeField] private int count = 1;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int layerNumber;
        [SerializeField] private SpawnStrategy strategy = SpawnStrategy.Zero;
        [SerializeField] private string desiredName = "";
        [SerializeField] private Vector3 optionalOffset = Vector3.zero;
        [SerializeField] private float minX = 0;
        [SerializeField] private float maxX = 0;
        [SerializeField] private float minZ = 0;
        [SerializeField] private float maxZ = 0;

        public void Spawn() => SpawnPrefabs();

        [ContextMenu("Spawn")]
        private void SpawnPrefabs()
        {
            var instantiated = 0;
            while (instantiated < count)
            {
                var gameObjectInstance = Instantiate(prefab);
                gameObjectInstance.name = desiredName == "" ? gameObjectInstance.name : desiredName;
                gameObjectInstance.layer = layerNumber;

                switch (strategy)
                {
                    case SpawnStrategy.Zero:
                        gameObjectInstance.transform.position = Vector3.zero;
                        break;
                    case SpawnStrategy.Random:
                        gameObjectInstance.transform.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)) + optionalOffset;
                        break;
                    case SpawnStrategy.Offset:
                        gameObjectInstance.transform.position = Vector3.zero + optionalOffset;
                        break;
                }

                PrefabSpawned?.Invoke(gameObjectInstance);
                instantiated++;
            }
        }
    }
}