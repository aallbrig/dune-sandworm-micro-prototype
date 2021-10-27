using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
        public GameObject[] sandwormBody = {};
        public Transform bodyParent;
        public string bodySegmentTag = "sandworm body segment";

        public BoneRenderer boneRenderer;
        public Rig rig;
        public RigBuilder rigBuilder;
        [SerializeField] private GameObject bodyPrefab;
        [SerializeField] private int desiredBodySegmentCount = 10;
        [SerializeField] private float distanceBetween = 3f;
        [SerializeField] private int layerMask = 7;
        private IMoveSandworms _sandwormMover;
        private GameObject[] _dampedTransforms = {};

        private void Start()
        {
            if (boneRenderer == null)
                Debug.LogError("Bone renderer component is required");

            if (rig == null)
                Debug.LogError("Rig component is required");

            if (rigBuilder == null)
                Debug.LogError("Rig builder component is required");

            _sandwormMover = GetComponent<IMoveSandworms>();
            GenerateBody();
        }

        [ContextMenu("Generate Body Segments")]
        public void GenerateBody()
        {
            ClearPossiblePreviousBodySegments();
            SpawnBodySegments();
            AddBodySegmentsToBoneRenderer();
            AddBodySegmentsToRig();
        }


        private void AddBodySegmentsToRig()
        {
            var dampedTransforms = new List<GameObject>();

            for (int index = 0; index < sandwormBody.Length; index++)
            {
                var bodySegment = sandwormBody[index];
                var targetGameObject = index == 0 ? rig.transform.GetChild(0).gameObject : new GameObject();

                if (index != 0)
                {
                    targetGameObject.name = "damped transform";
                    targetGameObject.tag = bodySegmentTag;
                    dampedTransforms.Add(targetGameObject);
                }

                if (targetGameObject.transform.parent != rig.transform)
                    targetGameObject.transform.SetParent(rig.transform);

                var sourceTransform = index == 0 ? sandwormHead.transform : sandwormBody[index - 1].transform;
                var constrainedTransform = bodySegment.transform;

                var dampedTransform = index == 0 ? targetGameObject.GetComponent<DampedTransform>() : targetGameObject.AddComponent<DampedTransform>();

                dampedTransform.weight = 0.5f;
                dampedTransform.data.sourceObject = sourceTransform;
                dampedTransform.data.constrainedObject = constrainedTransform;
                dampedTransform.data.constrainedObject = constrainedTransform;
                dampedTransform.data.dampPosition = 0.5f;
                dampedTransform.data.dampRotation = 0.5f;
                dampedTransform.data.maintainAim = true;
            }

            _dampedTransforms = dampedTransforms.ToArray();

            rigBuilder.Build();
        }

        [ContextMenu("Delete body segments")]
        private void ClearPossiblePreviousBodySegments()
        {
            foreach (var bodySegment in sandwormBody)
            {
                if (Application.isEditor) DestroyImmediate(bodySegment);
                else Destroy(bodySegment);
            }

            foreach (var dampedTransform in _dampedTransforms)
            {
                if (Application.isEditor) DestroyImmediate(dampedTransform);
                else Destroy(dampedTransform);
            }

            boneRenderer.transforms = new []
            {
                sandwormHead.transform
            };
        }

        private void SpawnBodySegments()
        {
            if (bodyPrefab == null || bodyParent == null) return;
            sandwormBody = new GameObject[desiredBodySegmentCount];

            var instantiated = 0;
            while (instantiated < desiredBodySegmentCount)
            {
                var bodyParent = instantiated == 0 ? sandwormHead.transform : sandwormBody[instantiated - 1].transform;
                var bodySegment = Instantiate(bodyPrefab, bodyParent);
                bodySegment.name = $"Sandworm Body Segment {instantiated} in {bodyParent.gameObject}";
                bodySegment.transform.position = bodyParent.position + Vector3.back * distanceBetween;
                bodySegment.tag = bodySegmentTag;

                sandwormBody[instantiated] = bodySegment;
                instantiated++;
            }
        }

        private void AddBodySegmentsToBoneRenderer()
        {
            var newTransforms = new Transform[desiredBodySegmentCount];
            var index = 0;

            foreach (var sandwormBodySegment in sandwormBody)
                newTransforms[index++] = sandwormBodySegment.transform;

            boneRenderer.transforms = boneRenderer.transforms.Concat(newTransforms).ToArray();
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