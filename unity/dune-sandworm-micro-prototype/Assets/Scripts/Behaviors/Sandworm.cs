using PlasticGui.Configuration.CloudEdition.Welcome;
using UnityEngine;

namespace Behaviors
{
    public class Sandworm : MonoBehaviour
    {
        public Vector3 Vector
        {
            get => Vector3.zero;
            set => Vector = value;
        }

        public void UpdateVector(Vector3 newVector)
        {
            Vector = newVector;
        }
    }
}