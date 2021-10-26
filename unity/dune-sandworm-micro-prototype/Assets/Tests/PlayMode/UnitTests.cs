using System.Collections;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SandwormHeadBehavior
    {
        private class EdibleObjectTestHarness: MonoBehaviour, IAmEdible
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

        [UnityTest]
        public IEnumerator CanEatEdibleObjects()
        {
            var sut = new GameObject().AddComponent<SandwormHead>();
            var testEdibleObject = new GameObject();
            var head = sut.GetComponent<SandwormHead>();
            var testHarness = testEdibleObject.AddComponent<EdibleObjectTestHarness>();
            yield return null;

            head.Eat(testEdibleObject);

            Assert.IsFalse(testHarness.canBeEaten);
            Assert.IsTrue(testHarness.hasBeenEaten);
        }
    }

    public class SandwormBehavior
    {
        public static Vector3[] DirectionVectors =
        {
            Vector3.forward,
            Vector3.left,
            Vector3.right,
            Vector3.back,
            Vector3.zero
        };

        [UnityTest]
        public IEnumerator SandwormCanHaveDirectionSet(
            [ValueSource(nameof(DirectionVectors))] Vector3 desiredDirection
        )
        {
            var sut = new GameObject().AddComponent<Sandworm>();
            yield return null;

            sut.UpdateTravelDirection(desiredDirection);
            yield return null;

            Assert.IsTrue(sut.TravelDirection == desiredDirection);
        }
    }
}