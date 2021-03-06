using System;
using ScriptableObjects;
using UnityEngine;

namespace Behaviors
{
    [Serializable]
    public class SandwormConfig
    {
        
        private SandwormConfig(float moveSpeed, float rotationSpeed)
        {
            MoveSpeed = moveSpeed;
            RotationSpeed = rotationSpeed;
        }

        public float MoveSpeed { get; private set; }

        public float RotationSpeed { get; private set; }

        public static SandwormConfig Of(float moveSpeed, float rotationSpeed) => new SandwormConfig(moveSpeed, rotationSpeed);
    }

    public interface IMoveSandworms
    {
        Vector3 TravelDirection { get; }
        void Move(SandwormConfig config, Vector3 directionOfTravel);
    }

    public interface IGenerateSandwormBody
    {
        void Generate(int length);
    }

    public class Sandworm : MonoBehaviour
    {
        public Vector3 TravelDirection => _sandwormMover.TravelDirection;
        public GameObject sandwormHead;
        public SandwormConfiguration config;

        [SerializeField] private int desiredBodySegmentCount = 8;
        [SerializeField] private int layerMask = 7;
        private IMoveSandworms _sandwormMover;
        private IGenerateSandwormBody _bodyGenerator;

        private void Start()
        {
            _bodyGenerator = GetComponent<IGenerateSandwormBody>();
            _sandwormMover = GetComponent<IMoveSandworms>();

            if (_bodyGenerator == null)
                Debug.LogError("A body generator is required!");

            if (_sandwormMover == null)
                Debug.LogError("A sandworm mover is required!");

            _bodyGenerator.Generate(desiredBodySegmentCount);
        }

        public void Move(Vector3 directionOfTravel) => _sandwormMover.Move(config.Get(), directionOfTravel);

        private void OnCollisionEnter(Collision other)
        {
            var otherGameObjectLayer = other.gameObject.layer;
            if (otherGameObjectLayer == layerMask || gameObject.layer == otherGameObjectLayer)
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }

        private void OnCollisionExit(Collision other)
        {
            var otherGameObjectLayer = other.gameObject.layer;
            if (otherGameObjectLayer == layerMask || gameObject.layer == otherGameObjectLayer)
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }

        private void OnCollisionStay(Collision other)
        {
            var otherGameObjectLayer = other.gameObject.layer;
            if (otherGameObjectLayer == layerMask || gameObject.layer == otherGameObjectLayer)
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }
    }
}