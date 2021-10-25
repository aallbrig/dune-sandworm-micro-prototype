using System.Collections;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Tests.PlayMode
{
    public class SpiceTilePrefab
    {
        private const string PrefabLocation = "Prefabs/Spice Tile";

        [UnityTest]
        public IEnumerator SitsOnTopOfGround()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation));
            yield return null;

            Assert.NotNull(sut.transform.position.y > 0);
        }

        [UnityTest]
        public IEnumerator HasAScore()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation));
            yield return null;

            var scoreComponent = sut.GetComponent<IHaveAScore>();

            Assert.NotNull(scoreComponent.Score);
        }

        [UnityTest]
        public IEnumerator CanBeEaten()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation));
            yield return null;
            var spice = sut.GetComponent<Spice>();
            var spiceAmount = spice.Amount;
            var edibleObject = sut.GetComponent<IAmEdible>();

            edibleObject.BeEaten();

            Assert.IsTrue(spiceAmount > spice.Amount);
        }
    }

    public class SandwormHeadPrefab
    {
        private const string PrefabLocation = "Prefabs/Sandworm Head";
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
        public IEnumerator CanEatEdibleObjectsViaCollision()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation));
            var testEdibleObject = new GameObject();
            testEdibleObject.AddComponent<BoxCollider>();
            var testHarness = testEdibleObject.AddComponent<EdibleObjectTestHarness>();
            testEdibleObject.transform.position = sut.transform.position;

            yield return new WaitForFixedUpdate(); // Allow collision to be detected

            Assert.IsFalse(testHarness.canBeEaten);
            Assert.IsTrue(testHarness.hasBeenEaten);
        }
    }

    public class SandwormPrefab
    {
        private const string PrefabLocation = "Prefabs/Sandworm";
        [UnityTest]
        public IEnumerator Exists()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation));
            yield return null;

            Assert.NotNull(sut);
        }
    }
}