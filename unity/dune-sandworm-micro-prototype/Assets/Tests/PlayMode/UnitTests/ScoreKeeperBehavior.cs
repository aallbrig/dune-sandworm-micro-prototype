using System.Collections;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class ScoreKeeperBehavior
    {

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

        private class SpyScoreRender : MonoBehaviour, IRenderScore
        {
            public string renderedScore = "";
            public void RenderScore(string scoreText) => renderedScore = scoreText;
        }

        private class SpyEdibleObject : MonoBehaviour, IAmEdible, IHaveAScore
        {
            private bool _hasBeenEaten;
            public bool CanBeEaten() => _hasBeenEaten != true;
            public void BeEaten() => _hasBeenEaten = true;

            public Score Score { get; set; }
        }
    }
}