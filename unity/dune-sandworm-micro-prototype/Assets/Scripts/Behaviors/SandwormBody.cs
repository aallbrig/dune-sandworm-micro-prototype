using UnityEngine;

namespace Behaviors
{
    public class SandwormBody : MonoBehaviour, IAmEdible, IHaveAScore
    {
        public int layerMask = 7;
        [SerializeField] private ParticleSystem onBeEatenParticles;

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

        public bool CanBeEaten() => true;
        public void BeEaten()
        {
            if (onBeEatenParticles) onBeEatenParticles.Play();
        }

        public Score Score => Score.Of(-10);
    }
}