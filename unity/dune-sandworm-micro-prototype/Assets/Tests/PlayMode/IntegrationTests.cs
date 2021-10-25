using System.Collections;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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

        [UnityTest]
        public IEnumerator Exists()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation));
            yield return null;

            Assert.NotNull(sut);
        }
    }
}