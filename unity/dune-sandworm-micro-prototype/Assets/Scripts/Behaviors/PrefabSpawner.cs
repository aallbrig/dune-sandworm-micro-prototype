using UnityEngine;

namespace Behaviors
{
    public class PrefabSpawner : MonoBehaviour, ICanSpawn
    {
        [SerializeField] private int count = 1;
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject targetPlane;
        [SerializeField] private int layerNumber;
        [SerializeField] private SpawnStrategy strategy = SpawnStrategy.Zero;
        [SerializeField] private string name = "";
        [SerializeField] private Vector3 optionalOffset = Vector3.zero;

        [ContextMenu("Spawn")]
        private void SpawnPrefabs()
        {
            var instantiated = 0;
            while (instantiated < count)
            {
                var gameObjectInstance = Instantiate(prefab);
                gameObjectInstance.name = name == "" ? gameObjectInstance.name : name;
                gameObjectInstance.layer = layerNumber;

                switch (strategy)
                {
                    case SpawnStrategy.Zero:
                        gameObjectInstance.transform.position = Vector3.zero;
                        break;
                    case SpawnStrategy.Random:
                        // TODO: Implement
                        gameObjectInstance.transform.position = targetPlane.transform.position;
                        break;
                    case SpawnStrategy.Offset:
                        gameObjectInstance.transform.position = Vector3.zero + optionalOffset;
                        break;
                }

                instantiated++;
            }
        }

        public void Spawn() => SpawnPrefabs();
    }
}