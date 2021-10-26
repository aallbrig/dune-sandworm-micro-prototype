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

        // Sandworm eats when its mouth collides with things

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

        private void HandleTrigger(Collider other) => Eat(other.gameObject);
    }
}