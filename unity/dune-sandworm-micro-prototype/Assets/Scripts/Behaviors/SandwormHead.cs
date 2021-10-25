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
        void BeEaten();
    }

    public class SandwormHead : MonoBehaviour
    {
        public static event Action<SandwormMeal> SandwormHasEaten;

        // Sandworm eats when its mouth collides with things
        private void OnCollisionEnter(Collision other)
        {
            var edibleObject = other.gameObject.GetComponent<IAmEdible>();
            if (edibleObject != null)
            {
                edibleObject.BeEaten();

                SandwormHasEaten?.Invoke(SandwormMeal.Of(gameObject, other.gameObject));
            }
        }
    }
}