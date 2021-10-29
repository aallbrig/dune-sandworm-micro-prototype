using System.Collections;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SpySandwormMover : MonoBehaviour, IMoveSandworms
    {

        public Vector3 TravelDirection { get; private set; }

        public void Move(Vector3 directionOfTravel) => TravelDirection = directionOfTravel;
    }
    
    public class FakeSandwormBodyGenerator: MonoBehaviour, IGenerateSandwormBody
    {
        public void Generate(int length) {}
    }

    public class EdibleObjectTestHarness: MonoBehaviour, IAmEdible
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


    public class SandwormHeadBehavior
    {
        [UnityTest]
        public IEnumerator CanEatEdibleObjects()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<BoxCollider>();
            var sut = gameObject.AddComponent<SandwormHead>();
            var testEdibleObject = new GameObject();
            var head = sut.GetComponent<SandwormHead>();
            var testHarness = testEdibleObject.AddComponent<EdibleObjectTestHarness>();
            yield return null;

            head.Eat(testEdibleObject);

            Assert.IsFalse(testHarness.canBeEaten);
            Assert.IsTrue(testHarness.hasBeenEaten);
        }
    }

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
            [ValueSource(nameof(DirectionVectors))] Vector3 desiredDirection
        )
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<FakeSandwormBodyGenerator>();
            var spy = gameObject.AddComponent<SpySandwormMover>();
            var sut = gameObject.AddComponent<Sandworm>();
            var otherGameObject = new GameObject();
            sut.sandwormHead = otherGameObject;
            yield return null;

            sut.Move(desiredDirection);
            yield return null;

            Assert.IsTrue(sut.TravelDirection == spy.TravelDirection);
        }
    }

    public class ScoreKeeperBehavior
    {
        private class SpyScoreRender: MonoBehaviour, IRenderScore
        {
            public string renderedScore = "";
            public void RenderScore(string scoreText) => renderedScore = scoreText;
        }
        private class SpyEdibleObject: MonoBehaviour, IAmEdible, IHaveAScore
        {
            public Score Score { get; set; }
            private bool _hasBeenEaten;
            public bool CanBeEaten() => _hasBeenEaten != true;
            public void BeEaten() => _hasBeenEaten = true;
        }

        [UnityTest]
        public IEnumerator ScoreKeeperRendersInitialScore()
        {
            var gameObject = new GameObject();
            var spy = gameObject.AddComponent<SpyScoreRender>();
            var sut = gameObject.AddComponent<ScoreKeeper>();

            yield return null;

            Assert.AreEqual(ScoreFormatter.Of(Score.Of(0)).ToString(), spy.renderedScore);
        }

        [UnityTest]
        public IEnumerator ScoreKeeperUsesScoreRendererToRenderNewScore()
        {
            var gameObject = new GameObject();
            var spyScoreRender = gameObject.AddComponent<SpyScoreRender>();
            var spyEdibleObject = new GameObject().AddComponent<SpyEdibleObject>();
            var targetScore = Score.Of(1337);
            spyEdibleObject.Score = targetScore;
            var sut = gameObject.AddComponent<ScoreKeeper>();
            var sandwormMeal = SandwormMeal.Of(new GameObject(), spyEdibleObject.gameObject);
            yield return null; // Allow unity lifecycle methods to be called

            SandwormHead.Eat(sandwormMeal);

            Assert.AreEqual(ScoreFormatter.Of(targetScore).ToString(), spyScoreRender.renderedScore);
        }
    }
}