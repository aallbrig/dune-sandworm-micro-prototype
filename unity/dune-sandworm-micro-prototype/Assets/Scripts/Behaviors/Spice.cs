using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Behaviors
{
    public class Spice : MonoBehaviour, IHaveAScore, IAmEdible
    {
        [SerializeField] private int amount = 10;
        [SerializeField] private float points = 1f;
        [SerializeField] private float eatDelay = 1f;
        [SerializeField] private ParticleSystem onBeEatenParticles;
        [SerializeField] private Renderer renderer;
        [SerializeField] private Material hitMaterial;
        [SerializeField] private float flashDelay = 0.16f;
        [SerializeField] private float flashCount = 2;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        private Material _originalMaterial;

        private IEnumerator _flashing;

        private float _lastEatenTime;

        public int Amount => amount;

        private void Start()
        {
            // Instantly edible
            _lastEatenTime = Time.time - eatDelay;
            renderer = renderer ? renderer : GetComponent<Renderer>();
            impulseSource = impulseSource ? impulseSource : GetComponent<CinemachineImpulseSource>();
            
            if (!renderer) Debug.LogError("Material is required for this behavior");
            if (!impulseSource) Debug.LogError("Cinemachine impulse source is required for this behavior");

            _originalMaterial = renderer.material;
        }

        public bool CanBeEaten() => amount > 0 && Time.time - _lastEatenTime > eatDelay;

        public void BeEaten()
        {
            if (!CanBeEaten()) return;

            amount -= 1;
            if (amount == 0)
                Destroy(gameObject, flashDelay * 2);
            _lastEatenTime = Time.time;
            _flashing = FlashingBehavior(_originalMaterial);
            StartCoroutine(_flashing);
            if (onBeEatenParticles) onBeEatenParticles.Play();
            if (impulseSource) impulseSource.GenerateImpulse();
        }

        private IEnumerator FlashingBehavior(Material originalMaterial)
        {
            for (int i = 0; i < flashCount; i++)
            {
                renderer.material = hitMaterial;
                yield return new WaitForSeconds(flashDelay);
                renderer.material = originalMaterial;
                yield return new WaitForSeconds(flashDelay);
            }
        }

        public Score Score => Score.Of(points);
    }
}