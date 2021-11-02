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
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float flashDelay = 0.16f;
        [SerializeField] private float flashCount = 2;
        [SerializeField] private CinemachineImpulseSource impulseSource;

        private IEnumerator _flashing;

        private float _lastEatenTime;

        public int Amount => amount;

        private void Start()
        {
            // Instantly edible
            _lastEatenTime = Time.time - eatDelay;
            meshRenderer = meshRenderer ? meshRenderer : GetComponent<MeshRenderer>();
            impulseSource = impulseSource ? impulseSource : GetComponent<CinemachineImpulseSource>();
            
            if (!meshRenderer) Debug.LogError("Material is required for this behavior");
            if (!impulseSource) Debug.LogError("Cinemachine impulse source is required for this behavior");
        }

        public bool CanBeEaten() => amount > 0 && Time.time - _lastEatenTime > eatDelay;

        public void BeEaten()
        {
            if (!CanBeEaten()) return;

            _lastEatenTime = Time.time;
            meshRenderer.material.color = MakeMaterialLighter(meshRenderer.material.color);
            _flashing = FlashingBehavior(meshRenderer.material.color);
            StartCoroutine(_flashing);
            if (onBeEatenParticles) onBeEatenParticles.Play();
            if (impulseSource) impulseSource.GenerateImpulse();
            amount -= 1;
            if (amount == 0)
                Destroy(gameObject);
        }
        private Color MakeMaterialLighter(Color color) => Color.Lerp(color, Color.white, .1f);

        private IEnumerator FlashingBehavior(Color originalColor)
        {
            for (int i = 0; i < flashCount; i++)
            {
                meshRenderer.material.color = Color.white;
                yield return new WaitForSeconds(flashDelay);
                meshRenderer.material.color = originalColor;
            }
        }

        public Score Score => Score.Of(points);
    }
}