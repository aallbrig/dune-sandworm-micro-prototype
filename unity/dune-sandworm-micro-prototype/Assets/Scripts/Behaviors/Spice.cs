using UnityEngine;

namespace Behaviors
{
    public class Spice : MonoBehaviour, IHaveAScore
    {

        [SerializeField] private float points = 1f;

        public Score Score => Score.Of(points);
    }
}