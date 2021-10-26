using Cinemachine;
using UnityEngine;

namespace Behaviors
{
    [RequireComponent(typeof(PrefabSpawner))]
    public class SetFollowTarget : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private PrefabSpawner _prefabSpawner;
        private void OnEnable()
        {
            if (virtualCamera == null)
            {
                Debug.LogError("Virtual camera not set and is required");
            }
            _prefabSpawner = GetComponent<PrefabSpawner>();
            _prefabSpawner.PrefabSpawned += UpdateFollowTarget;
        }

        private void UpdateFollowTarget(GameObject spawnedGameObject)
        {
            virtualCamera.Follow = spawnedGameObject.transform;
        }
    }
}