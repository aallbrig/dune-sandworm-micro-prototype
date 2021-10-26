using UnityEngine;

namespace Behaviors.SandwormMovement
{
    public class TransformMovement : MonoBehaviour, IMoveSandworms
    {

        public Vector3 TravelDirection { get; private set; }
        [SerializeField] private float speed = 100f;
        [SerializeField] private float rotateSpeed = 6f;
        [SerializeField] private Transform selfTransform;

        public void Move(Vector3 directionOfTravel) => TravelDirection = directionOfTravel;
        private void Start()
        {
            selfTransform = transform;
        }

        private void Update()
        {
            if (TravelDirection == Vector3.zero) return;

            selfTransform.rotation = Quaternion.Slerp(selfTransform.rotation, Quaternion.LookRotation(TravelDirection), rotateSpeed * Time.deltaTime);
            transform.position += selfTransform.rotation * Vector3.forward * Time.deltaTime * speed;
        }
    }
}