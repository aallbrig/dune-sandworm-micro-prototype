using Behaviors;
using UnityEngine;

namespace Tests.PlayMode
{
    public class FakeSandwormBodyGenerator: MonoBehaviour, IGenerateSandwormBody
    {
        public void Generate(int length) {}
    }
}