using System;
using UnityEngine;

namespace Behaviors
{
    public interface IAmEdible
    {
        void BeEaten();
    }

    public class SandwormHead : MonoBehaviour
    {
        public static event Action<SandwormHead, GameObject> SandwormHasEaten;

        // Sandworm eats when its mouth collides with things
        public void OnCollisionEnter(Collision other)
        {
            var edibleObject = other.gameObject.GetComponent<IAmEdible>();
            if (edibleObject != null)
            {
                edibleObject.BeEaten();
                SandwormHasEaten?.Invoke(this, other.gameObject);
            }

        }
    }
}