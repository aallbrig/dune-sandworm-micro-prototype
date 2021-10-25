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
}