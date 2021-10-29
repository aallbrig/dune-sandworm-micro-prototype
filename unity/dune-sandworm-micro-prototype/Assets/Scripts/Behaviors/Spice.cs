using UnityEngine;

namespace Behaviors
{
    public class Spice : MonoBehaviour, IHaveAScore, IAmEdible
    {
        [SerializeField] private int amount = 10;
        [SerializeField] private float points = 1f;
        [SerializeField] private float eatDelay = 1f;
        [SerializeField] private ParticleSystem onBeEatenParticles;

        private float _lastEatenTime;

        public int Amount => amount;

        private void Start() =>
            // Just set it such that a player can instantly eat it without delay to prevent need of null check
            _lastEatenTime = Time.time - eatDelay;

        public bool CanBeEaten() => amount > 0 && Time.time - _lastEatenTime > eatDelay;

        public void BeEaten()
        {
            if (!CanBeEaten()) return;

            _lastEatenTime = Time.time;
            if (onBeEatenParticles) onBeEatenParticles.Play();
            amount -= 1;
            if (amount == 0)
                Destroy(gameObject);
        }

        public Score Score => Score.Of(points);
    }
}