using UnityEngine;

namespace Behaviors.SandwormMovement
{
    public class TransformMovement : MonoBehaviour, IMoveSandworms
    {

        public Vector3 TravelDirection { get; private set; }
        [SerializeField] private float speed = 100f;
        [SerializeField] private float rotateSpeed = 6f;
        [SerializeField] private Transform targetTransform;

        public void Move(Vector3 directionOfTravel) => TravelDirection = directionOfTravel;
        private void Start()
        {
            targetTransform = targetTransform ? targetTransform : transform;
        }

        private void Update()
        {
            if (TravelDirection == Vector3.zero) return;

            targetTransform.rotation = Quaternion.Slerp(targetTransform.rotation, Quaternion.LookRotation(TravelDirection), rotateSpeed * Time.deltaTime);
            targetTransform.position += targetTransform.rotation * Vector3.forward * Time.deltaTime * speed;
        }
    }
}