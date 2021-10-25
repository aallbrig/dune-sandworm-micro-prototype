using UnityEngine;

namespace Behaviors
{
    public class CluelessNavigator : MonoBehaviour, IHaveAScore
    {
        [SerializeField] private const float Points = 10f;
        public Score Score { get; } = Score.Of(Points);

        // As a step
        public void BeEaten()
        {
            // Yell?
        }
    }
}