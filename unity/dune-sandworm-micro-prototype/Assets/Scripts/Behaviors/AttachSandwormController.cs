using UnityEngine;

namespace Behaviors
{
    [RequireComponent(typeof(PrefabSpawner))]
    public class AttachSandwormController : MonoBehaviour
    {
        private PrefabSpawner _prefabSpawner;
        private void OnEnable()
        {
            _prefabSpawner = GetComponent<PrefabSpawner>();
            _prefabSpawner.PrefabSpawned += AttachController;
        }

        private void AttachController(GameObject spawnedGameObject)
        {
            spawnedGameObject.AddComponent<SandwormController>();
        }
    }
}