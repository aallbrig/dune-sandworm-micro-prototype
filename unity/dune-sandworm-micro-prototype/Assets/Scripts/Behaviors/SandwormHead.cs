using System;
using UnityEngine;

namespace Behaviors
{
    public class SandwormMeal
    {
        public GameObject Sandworm { get; }
        public GameObject EdibleObject { get; }

        public static SandwormMeal Of(GameObject sandworm, GameObject edibleObject) => new SandwormMeal(sandworm, edibleObject);

        private SandwormMeal(GameObject sandworm, GameObject edibleObject)
        {
            Sandworm = sandworm;
            EdibleObject = edibleObject;
        }
    }

    public interface IAmEdible
    {
        bool CanBeEaten();
        void BeEaten();
    }

    public class SandwormHead : MonoBehaviour
    {
        public static event Action<SandwormMeal> SandwormHasEaten;

        // Sandworm eats when its mouth collides with things
        public void Eat(GameObject maybeEdibleObject)
        {
            var edibleObject = maybeEdibleObject.GetComponent<IAmEdible>();
            if (edibleObject != null && edibleObject.CanBeEaten())
            {
                edibleObject.BeEaten();

                SandwormHasEaten?.Invoke(SandwormMeal.Of(gameObject, maybeEdibleObject));
            }
        }

        private void OnCollisionStay(Collision other)
        {
            Eat(other.gameObject);
        }
    }
}