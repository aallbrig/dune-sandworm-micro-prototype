using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviors
{
    public enum TextType
    {
        PRIMARY,
        HINT
    }
    public class StyleGuide : MonoBehaviour
    {
        [SerializeField] private UserInterfaceConfiguration uiConfig;
        [SerializeField] private List<GameObject> targets = new List<GameObject>();
        [SerializeField] private TextType textType = TextType.PRIMARY;

        private void Start() => ApplyStyles();

        [ContextMenu("Apply Styles")]
        public void ApplyStyles()
        {
            if (!uiConfig) return;

            foreach (var target in targets)
            {
                var maybeText = target.GetComponent<Text>();

                if (maybeText) maybeText.color = Of(textType);
            }
        }

        private Color Of(TextType type)
        {
            Color returnColor;

            switch (type)
            {
                case TextType.PRIMARY:
                    returnColor = uiConfig.primaryColor;
                    break;
                case TextType.HINT:
                    returnColor = uiConfig.hintColor;
                    break;
                default:
                    returnColor = Color.black;
                    break;
            }

            return returnColor;
        }
    }
}