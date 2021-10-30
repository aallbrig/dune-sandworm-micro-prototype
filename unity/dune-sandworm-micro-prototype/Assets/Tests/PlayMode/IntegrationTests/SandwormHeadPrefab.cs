using System.Collections;
using Behaviors;
using NUnit.Framework;
using Tests.PlayMode.Utilities;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SandwormHeadPrefab
    {
        private const string PrefabLocation = "Prefabs/Sandworm Head";

        [UnityTest]
        public IEnumerator CanEatEdibleObjectsViaCollision()
        {
            var testEdibleObject = new GameObject();
            testEdibleObject.AddComponent<BoxCollider>();
            var rigidBody = testEdibleObject.AddComponent<Rigidbody>();
            rigidBody.useGravity = false;
            var spy = testEdibleObject.AddComponent<SpyEdibleObject>();
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation), TestLocation.Next());

            testEdibleObject.transform.position = sut.GetComponent<Collider>().bounds.center;
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(spy.hasBeenEaten);
        }

        private class SpyEdibleObject : MonoBehaviour, IAmEdible
        {
            public bool canBeEaten = true;
            public bool hasBeenEaten;
            public bool CanBeEaten() => canBeEaten;
            public void BeEaten()
            {
                canBeEaten = false;
                hasBeenEaten = true;
            }
        }
    }
}