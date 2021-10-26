using System;
using UnityEngine;

namespace Behaviors
{
    public class SandwormBody : MonoBehaviour
    {
        public int layerMask = 7;
        [SerializeField] private int layer = 6;
        [SerializeField] private Rigidbody rigidbody;
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
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

        private void OnCollisionExit(Collision other)
        {
            var otherGameObjectLayer = other.gameObject.layer;
            if (otherGameObjectLayer == layerMask || gameObject.layer == otherGameObjectLayer)
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }
    }
}