using UnityEngine;

namespace Behaviors
{
    public class Sandworm : MonoBehaviour
    {
        [SerializeField] private Transform bodyParent;
        [SerializeField] private GameObject bodyPrefab;
        [SerializeField] private int desiredBodySegmentCount = 10;
        [SerializeField] private float distanceBetween = 3f;
        [SerializeField] private Vector3 directionOfTravel = Vector3.zero;
        [SerializeField] private int layerMask = 7;

        public Vector3 DirectionOfTravel => directionOfTravel;

        public void UpdateDirectionOfTravel(Vector3 newVector) => directionOfTravel = newVector;

        [ContextMenu("Generate Body Segments")]
        public void GenerateBody()
        {
            // DeleteAllBodySegments();
            SpawnBodySegments();
        }

        private void Start()
        {
            GenerateBody();
        }
        private void DeleteAllBodySegments()
        {
            foreach (Transform child in bodyParent) {
                DestroyImmediate(child.gameObject);
            }
        }

        private void SpawnBodySegments()
        {
            if (bodyPrefab == null || bodyParent == null) return;

            var instantiated = 0;
            while (instantiated < desiredBodySegmentCount)
            {
                var bodySegment = Instantiate(bodyPrefab, bodyParent);
                bodySegment.name = $"Sandworm Body Segment {instantiated}";
                bodySegment.transform.position = bodyParent.position + Vector3.back * distanceBetween * (instantiated + 1);
                bodySegment.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bodySegment.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                bodySegment.GetComponent<SandwormBody>().layerMask = layerMask;

                instantiated++;
            }
        }
    }
}