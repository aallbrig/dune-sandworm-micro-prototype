using System;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviors
{
    public interface ICanSpawn
    {
        void Spawn();
    }

    [Serializable]
    public enum SpawnStrategy
    {
        Zero,
        Random,
        Offset
    }

    public class Spawner : MonoBehaviour
    {
        private readonly List<ICanSpawn> _spawns = new List<ICanSpawn>();

        private void Start()
        {
            _spawns.AddRange(GetComponents<ICanSpawn>());
            Spawn();
        }

        private void Spawn()
        {
            foreach (var spawn in _spawns) spawn.Spawn();
        }
    }
}