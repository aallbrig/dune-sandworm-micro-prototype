using Cinemachine;
using UnityEngine;

namespace Behaviors
{
    [RequireComponent(typeof(PrefabSpawner))]
    public class SetupGameplayCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private PrefabSpawner prefabSpawner;

        private void OnEnable()
        {
            prefabSpawner = prefabSpawner ?  prefabSpawner : GetComponent<PrefabSpawner>();

            if (virtualCamera == null) Debug.LogError("Virtual camera is required!");
            if (prefabSpawner == null) Debug.LogError("Virtual camera is required!");

            prefabSpawner.PrefabSpawned += UpdateFollowTarget;
        }

        private void UpdateFollowTarget(GameObject spawnedGameObject)
        {
            var sandworm = spawnedGameObject.GetComponent<Sandworm>();

            if (sandworm)
            {
                virtualCamera.Follow = sandworm.sandwormHead.transform;
                virtualCamera.LookAt = sandworm.sandwormHead.transform;
            }
        }
    }
}