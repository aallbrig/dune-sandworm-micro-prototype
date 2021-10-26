using System;
using UnityEngine;

namespace Behaviors
{
    public class SandwormMeal
    {
        public static SandwormMeal Of(GameObject sandworm, GameObject edibleObject) =>
            new SandwormMeal(sandworm, edibleObject);

        private SandwormMeal(GameObject sandworm, GameObject edibleObject)
        {
            Sandworm = sandworm;
            EdibleObject = edibleObject;
        }

        public GameObject Sandworm { get; }

        public GameObject EdibleObject { get; }
    }

    public interface IAmEdible
    {
        bool CanBeEaten();
        void BeEaten();
    }

    [RequireComponent(typeof(Collider))]
    public class SandwormHead : MonoBehaviour
    {
        public static event Action<SandwormMeal> SandwormHasEaten;

        [SerializeField] private int layer = 6;
        [SerializeField] private int layerMask = 7;
        [SerializeField] private Collider selfCollider;

        // Sandworm eats when its mouth collides with things
        private void Awake() => gameObject.layer = layer;
        private void Start()
        {
            selfCollider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision other) => HandleCollisions(other);
        private void OnCollisionExit(Collision other) => HandleCollisions(other);
        private void OnCollisionStay(Collision other) => HandleCollisions(other);
        private void OnTriggerEnter(Collider other) => HandleTrigger(other);
        private void OnTriggerExit(Collider other) => HandleTrigger(other);
        private void OnTriggerStay(Collider other) => HandleTrigger(other);

        public void Eat(GameObject maybeEdibleObject)
        {
            var edibleObject = maybeEdibleObject.GetComponent<IAmEdible>();
            if (edibleObject != null && edibleObject.CanBeEaten())
            {
                edibleObject.BeEaten();

                SandwormHasEaten?.Invoke(SandwormMeal.Of(gameObject, maybeEdibleObject));
            }
        }

        private void HandleCollisions(Collision other)
        {
            var otherGameObjectLayer = other.gameObject.layer;
            if (otherGameObjectLayer == layerMask || gameObject.layer == otherGameObjectLayer)
                Physics.IgnoreCollision(other.collider, selfCollider);
            else Eat(other.gameObject);
        }

        private void HandleTrigger(Collider other) => Eat(other.gameObject);
    }
}