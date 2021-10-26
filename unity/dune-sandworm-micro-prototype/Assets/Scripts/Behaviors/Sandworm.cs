using UnityEngine;

namespace Behaviors
{
    public interface IMoveSandworms
    {
        Vector3 TravelDirection { get; }
        void Move(Vector3 directionOfTravel);
    }

    public class Sandworm : MonoBehaviour, IMoveSandworms
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
            _sandwormMover = sandwormHead.GetComponent<IMoveSandworms>();
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
                DestroyImmediate(child.gameObject);
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
    }
}