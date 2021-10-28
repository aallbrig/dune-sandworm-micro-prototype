using System;
using UnityEngine;

namespace Behaviors
{
    public class ScoreFormatter
    {
        public static ScoreFormatter Of(Score score) => new ScoreFormatter(score);
        private ScoreFormatter(Score score)
        {
            _formattedText = string.Format(_format, score);
        }

        private readonly string _format = "Score: {0,9}";
        private readonly string _formattedText;

        public override string ToString() => _formattedText;
    }
    [Serializable]
    public class Score
    {

        private Score(float points) => Points = points;

        public float Points { get; private set; }

        public static Score Of(float points) => new Score(points);

        public override string ToString() => Points.ToString();
    }

    public interface IHaveAScore
    {
        Score Score { get; }
    }

    public interface IRenderScore
    {
        void RenderScore(string scoreText);
    }

    public class ScoreKeeper : MonoBehaviour
    {

        public static event Action<Score> NewScoreWasCalculated;

        private Score _score = Score.Of(0);
        private IRenderScore scoreRenderer;

        private void OnEnable()
        {
            NewScoreWasCalculated += RenderNewScore;
            SandwormHead.SandwormHasEaten += CalculateNewScore;
        }

        private void OnDisable()
        {
            NewScoreWasCalculated -= RenderNewScore;
            SandwormHead.SandwormHasEaten -= CalculateNewScore;
        }

        private void RenderNewScore(Score newScore) => scoreRenderer.RenderScore(ScoreFormatter.Of(_score).ToString());

        private void Start()
        {
            scoreRenderer = GetComponent<IRenderScore>();
            
            if (scoreRenderer == null) Debug.LogError("Score renderer required component for score keeper!");
            
            RenderNewScore(_score); // Initial render
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