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

        private const string PrefabLocation = "Prefabs/Sandworm Head";

        [UnityTest]
        public IEnumerator CanEatEdibleObjects()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation));
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