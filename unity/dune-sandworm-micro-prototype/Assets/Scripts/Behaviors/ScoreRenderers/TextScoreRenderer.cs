using UnityEngine;
using UnityEngine.UI;

namespace Behaviors.ScoreRenderers
{
    public class TextScoreRenderer : MonoBehaviour, IRenderScore
    {
        [SerializeField] private Text text;

        private void Start()
        {
            text = text ? text : GetComponent<Text>();

            if (text == null) Debug.LogError("Text component is required for Text Score Renderer");
        }

        public void RenderScore(string scoreText)
        {
            if (text != null)
                text.text = scoreText;
        }
    }
}
