using UnityEngine;

namespace Behaviors.SandwormMovement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour, IMoveSandworms
    {
        [SerializeField] private ForceMode forceMode = ForceMode.Acceleration;
        [SerializeField] private float speed = 100f;
        [SerializeField] private Rigidbody selfRigidbody;
        [SerializeField] private Vector3 force;
        private void Start()
        {
            selfRigidbody = GetComponent<Rigidbody>();
        }

        private void LateUpdate()
        {
            selfRigidbody.AddForce(force * Time.deltaTime, forceMode);
            var transformRotation = transform.rotation;
            transformRotation.x = 0;
            transform.rotation = transformRotation;
        }

        public Vector3 TravelDirection => selfRigidbody.velocity.normalized;

        public void Move(SandwormConfig config, Vector3 directionOfTravel) => force = directionOfTravel * speed;
    }
}