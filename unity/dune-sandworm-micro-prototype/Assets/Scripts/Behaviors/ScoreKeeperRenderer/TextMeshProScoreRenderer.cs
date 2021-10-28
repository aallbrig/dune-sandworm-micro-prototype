using System;
using TMPro;
using UnityEngine;

namespace Behaviors.ScoreKeeperRenderer
{
    public class TextMeshProScoreRenderer : MonoBehaviour, IRenderScore
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;

        private void Start()
        {
            textMeshPro = textMeshPro ? textMeshPro : GetComponent<TextMeshProUGUI>();

            if (textMeshPro == null) Debug.LogError("Text Mesh Pro component is required for TMPScoreRenderer");
        }

        public void RenderScore(string scoreText) => textMeshPro.SetText(scoreText);
    }
}