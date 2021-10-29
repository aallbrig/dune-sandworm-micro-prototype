using UnityEngine;

namespace Behaviors.SandwormMovement
{
    public class TransformMovement : MonoBehaviour, IMoveSandworms
    {
        public Vector3 TravelDirection { get; private set; }
        [SerializeField] private Transform targetTransform;
        private SandwormConfig _config;

        public void Move(SandwormConfig config, Vector3 directionOfTravel)
        {
            _config = config;
            TravelDirection = directionOfTravel;
        }

        private void Start()
        {
            targetTransform = targetTransform ? targetTransform : transform;
        }

        private void Update()
        {
            if (TravelDirection == Vector3.zero) return;

            targetTransform.rotation = Quaternion.Slerp(
                targetTransform.rotation,
                Quaternion.LookRotation(TravelDirection),
                _config.RotationSpeed * Time.deltaTime
            );

            targetTransform.position += targetTransform.rotation * Vector3.forward * Time.deltaTime * _config.MoveSpeed;
        }
    }
}