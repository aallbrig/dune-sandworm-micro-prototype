using System.Collections;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.Utilities
{
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
            var testHarness = testEdibleObject.AddComponent<SpyEdibleObject>();
            yield return null;

            head.Eat(testEdibleObject);

            Assert.IsFalse(testHarness.canBeEaten);
            Assert.IsTrue(testHarness.hasBeenEaten);
        }
    }
}