using Behaviors;
using UnityEngine;

namespace Tests.PlayMode
{
    public class SpySandwormMover : MonoBehaviour, IMoveSandworms
    {

        public Vector3 TravelDirection { get; private set; }
        public SandwormConfig Config { get; private set; }

        public void Move(SandwormConfig config, Vector3 directionOfTravel)
        {
            Config = config;
            TravelDirection = directionOfTravel;
        }
    }
}