using System.Collections;
using NUnit.Framework;
using Tests.PlayMode.Utilities;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.IntegrationTests
{
    public class SandwormSoundOrganPrefab
    {
        private const string PrefabLocation = "Prefabs/Sandworm Sound Organ";
        [UnityTest]
        public IEnumerator Exists()
        {
            var sut = Object.Instantiate(Resources.Load<GameObject>(PrefabLocation), TestLocation.Next());
            yield return null;

            Assert.NotNull(sut);
        }
    }
}