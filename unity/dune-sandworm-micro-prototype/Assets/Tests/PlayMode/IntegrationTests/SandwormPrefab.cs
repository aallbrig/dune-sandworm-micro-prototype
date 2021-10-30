using System.Collections;
using NUnit.Framework;
using Tests.PlayMode.Utilities;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SandwormPrefab
    {
        private const string PrefabLocation = "Prefabs/Sandworm";
        [UnityTest]
        public IEnumerator Exists()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation), TestLocation.Next());
            yield return null;

            Assert.NotNull(sut);
        }
    }
}