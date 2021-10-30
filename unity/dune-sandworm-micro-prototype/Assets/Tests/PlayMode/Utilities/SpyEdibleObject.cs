using Behaviors;
using UnityEngine;

namespace Tests.PlayMode
{
    public class SpyEdibleObject: MonoBehaviour, IAmEdible
    {
        public bool canBeEaten = true;
        public bool hasBeenEaten = false;
        public bool CanBeEaten() => canBeEaten;
        public void BeEaten()
        {
            canBeEaten = false;
            hasBeenEaten = true;
        } 
    }
}