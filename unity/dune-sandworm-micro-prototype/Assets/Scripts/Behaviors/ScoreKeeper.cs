using System;
using UnityEngine;

namespace Behaviors
{
    [Serializable]
    public class Score
    {
        public static Score Of(float points) => new Score(points);
        public float Points { get; private set; }

        private Score(float points)
        {
            Points = points;
        }
    }

    public interface IHaveAScore
    {
        Score Score { get; }
    }

    public class ScoreKeeper : MonoBehaviour
    {
        public static event Action<Score> NewScoreWasCalculated;

        private Score _score = Score.Of(0);

        private void OnEnable()
        {
            SandwormHead.SandwormHasEaten += CalculateNewScore;
        }

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