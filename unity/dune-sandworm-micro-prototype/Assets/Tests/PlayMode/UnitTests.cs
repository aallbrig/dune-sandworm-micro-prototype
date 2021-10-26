using System.Collections;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SandwormMoverTestHarness : MonoBehaviour, IMoveSandworms
    {

        public Vector3 TravelDirection { get; private set; }

        public void Move(Vector3 directionOfTravel) => TravelDirection = directionOfTravel;
    }
    
    public class EdibleObjectTestHarness: MonoBehaviour, IAmEdible
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


    public class SandwormHeadBehavior
    {
        [UnityTest]
        public IEnumerator CanEatEdibleObjects()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<BoxCollider>();
            var sut = gameObject.AddComponent<SandwormHead>();
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
            var otherGameObject = new GameObject();
            var gameObject = new GameObject();
            var testHarness = otherGameObject.AddComponent<SandwormMoverTestHarness>();
            var sut = gameObject.AddComponent<Sandworm>();
            sut.sandwormHead = otherGameObject;
            sut.bodyParent = otherGameObject.transform;
            yield return null;

            sut.Move(desiredDirection);
            yield return null;

            Assert.IsTrue(sut.TravelDirection == testHarness.TravelDirection);
        }
    }
}