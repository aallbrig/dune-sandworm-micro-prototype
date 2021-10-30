using System.Collections;
using Behaviors;
using NUnit.Framework;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SandwormBehavior
    {
        public static Vector3[] DirectionVectors =
        {
            Vector3.forward,
            Vector3.left,
            Vector3.right,
            Vector3.back,
            Vector3.zero
        };

        [UnityTest]
        public IEnumerator SandwormCanHaveDirectionSet(
            [ValueSource(nameof(DirectionVectors))]
            Vector3 desiredDirection
        )
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<FakeSandwormBodyGenerator>();
            var spy = gameObject.AddComponent<SpySandwormMover>();
            var sut = gameObject.AddComponent<Sandworm>();
            sut.config = ScriptableObject.CreateInstance<SandwormConfiguration>();
            sut.sandwormHead = new GameObject();
            yield return null;

            sut.Move(desiredDirection);
            yield return null;

            Assert.IsTrue(sut.TravelDirection == spy.TravelDirection);
        }
    }
}