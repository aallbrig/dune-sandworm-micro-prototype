using UnityEngine;
using UnityEngine.UI;

namespace Behaviors
{
    public class ScoreRenderer : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private string pre = "Points: ";
        [SerializeField] private string post = "";

        private void OnEnable()
        {
            ScoreKeeper.NewScoreWasCalculated += Render;
        }

        public void Render(Score score) => text.text = $"{pre}{score.Points.ToString()}{post}";
    }
}