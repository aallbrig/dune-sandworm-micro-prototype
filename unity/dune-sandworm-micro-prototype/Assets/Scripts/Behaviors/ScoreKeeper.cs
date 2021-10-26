using System;
using UnityEngine;

namespace Behaviors
{
    [Serializable]
    public class Score
    {

        private Score(float points) => Points = points;

        public float Points { get; private set; }

        public static Score Of(float points) => new Score(points);
    }

    public interface IHaveAScore
    {
        Score Score { get; }
    }

    public class ScoreKeeper : MonoBehaviour
    {

        private Score _score = Score.Of(0);

        private void OnEnable() => SandwormHead.SandwormHasEaten += CalculateNewScore;

        public static event Action<Score> NewScoreWasCalculated;

        private void CalculateNewScore(SandwormMeal meal)
        {
            var scoreableObject = meal.EdibleObject.GetComponent<IHaveAScore>();
            if (scoreableObject == null) return;

            var additionalPoints = scoreableObject.Score;
            _score = Score.Of(_score.Points + additionalPoints.Points);

            NewScoreWasCalculated?.Invoke(_score);
        }
    }
}