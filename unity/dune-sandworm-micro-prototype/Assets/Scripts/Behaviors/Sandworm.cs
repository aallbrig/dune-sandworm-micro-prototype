using UnityEngine;

namespace Behaviors
{
    public interface IMoveSandworms
    {
        Vector3 TravelDirection { get; }
        void Move(Vector3 directionOfTravel);
    }

    public class Sandworm : MonoBehaviour
    {
        public Vector3 TravelDirection => _sandwormMover.TravelDirection;
        public GameObject sandwormHead;
        public GameObject[] sandwormBody;
        public Transform bodyParent;

        [SerializeField] private GameObject bodyPrefab;
        [SerializeField] private int desiredBodySegmentCount = 10;
        [SerializeField] private float distanceBetween = 3f;
        [SerializeField] private int layerMask = 7;
        private IMoveSandworms _sandwormMover;

        private void Start()
        {
            _sandwormMover = GetComponent<IMoveSandworms>();
            GenerateBody();
        }

        [ContextMenu("Generate Body Segments")]
        public void GenerateBody()
        {
            DeleteAllBodySegments();
            SpawnBodySegments();
        }

        private void DeleteAllBodySegments()
        {
            foreach (Transform child in bodyParent)
            {
                Destroy(child.gameObject);
                DestroyImmediate(child.gameObject);
            }
        }

        private void SpawnBodySegments()
        {
            if (bodyPrefab == null || bodyParent == null) return;
            sandwormBody = new GameObject[desiredBodySegmentCount];

            var instantiated = 0;
            while (instantiated < desiredBodySegmentCount)
            {
                var bodySegment = Instantiate(bodyPrefab, bodyParent);
                bodySegment.name = $"Sandworm Body Segment {instantiated}";
                bodySegment.transform.position = bodyParent.position + Vector3.back * distanceBetween * (instantiated + 1);
                bodySegment.GetComponent<SandwormBody>().layerMask = layerMask;

                sandwormBody[instantiated] = bodySegment;
                instantiated++;
            }
        }

        public void Move(Vector3 directionOfTravel) => _sandwormMover.Move(directionOfTravel);

        private void OnCollisionEnter(Collision other)
        {
            var otherGameObjectLayer = other.gameObject.layer;
            if (otherGameObjectLayer == layerMask || gameObject.layer == otherGameObjectLayer)
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }

        private void OnCollisionExit(Collision other)
        {
            var otherGameObjectLayer = other.gameObject.layer;
            if (otherGameObjectLayer == layerMask || gameObject.layer == otherGameObjectLayer)
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }

        private void OnCollisionStay(Collision other)
        {
            var otherGameObjectLayer = other.gameObject.layer;
            if (otherGameObjectLayer == layerMask || gameObject.layer == otherGameObjectLayer)
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }
    }
}