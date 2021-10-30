using System.Collections;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.UnitTests
{
    public class SandwormSoundSystemBehavior
    {
        [UnityTest]
        public IEnumerator Exists()
        {
            yield return null;
            Assert.NotNull(new GameObject().AddComponent<SandwormSoundSystem>());
        }
    }
}