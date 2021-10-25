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
        public IEnumerator CanBeEaten()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation));
            yield return null;

            var scoreComponent = sut.GetComponent<IHaveAScore>();

            Assert.NotNull(scoreComponent.Score);
        }
    }
}