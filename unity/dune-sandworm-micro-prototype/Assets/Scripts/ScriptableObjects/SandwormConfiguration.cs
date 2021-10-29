using Behaviors;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Sandworm Config", menuName = "SOD/new Sandworm Configuration", order = 0)]
    public class SandwormConfiguration : ScriptableObject
    {
        [SerializeField] private float moveSpeed = 12f;
        [SerializeField] private float rotationSpeed = 0.25f;

        public SandwormConfig Get() => SandwormConfig.Of(moveSpeed, rotationSpeed);
    }
}