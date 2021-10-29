using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Behaviors.SandwormBodyGenerators
{
    public class GenerateAndRig : MonoBehaviour, IGenerateSandwormBody
    {
        private GameObject[] _sandwormBody = {};
        private GameObject[] _dampedTransforms = {};

        [SerializeField] private Transform origin;
        [SerializeField] private Rig rig;
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private GameObject bodyPrefab;
        [SerializeField] private string bodySegmentTag = "sandworm body segment";
        [SerializeField] private float distanceBetween = 2f;

        [ContextMenu("Generate Body Segments")]
        public void Generate(int length) => GenerateBody(length);

        private void Start()
        {
            rig = rig ? rig : GetComponent<Rig>();
            rigBuilder = rigBuilder ? rigBuilder : GetComponent<RigBuilder>();

            if (rig == null)
                Debug.LogError("Rig component is required");

            if (rigBuilder == null)
                Debug.LogError("Rig builder component is required");
        }

        private void GenerateBody(int length)
        {
            ClearPossiblePreviousBodySegments();
            SpawnBodySegments(length);
            AddBodySegmentsToBoneRenderer(length);
            AddBodySegmentsToRig();
        }

        private void AddBodySegmentsToRig()
        {
            var dampedTransforms = new List<GameObject>();

            for (int index = 0; index < _sandwormBody.Length; index++)
            {
                var bodySegment = _sandwormBody[index];
                var targetGameObject = index == 0 ? rig.transform.GetChild(0).gameObject : new GameObject();

                if (index != 0)
                {
                    targetGameObject.name = "damped transform";
                    targetGameObject.tag = bodySegmentTag;
                    dampedTransforms.Add(targetGameObject);
                }

                if (targetGameObject.transform.parent != rig.transform)
                    targetGameObject.transform.SetParent(rig.transform);

                var sourceTransform = index == 0 ? origin : _sandwormBody[index - 1].transform;
                var constrainedTransform = bodySegment.transform;

                var dampedTransform = index == 0 ? targetGameObject.GetComponent<DampedTransform>() : targetGameObject.AddComponent<DampedTransform>();

                dampedTransform.weight = 0.5f;
                dampedTransform.data.sourceObject = sourceTransform;
                dampedTransform.data.constrainedObject = constrainedTransform;
                dampedTransform.data.constrainedObject = constrainedTransform;
                dampedTransform.data.dampPosition = 0.5f;
                dampedTransform.data.dampRotation = 0f;
                dampedTransform.data.maintainAim = true;
            }

            _dampedTransforms = dampedTransforms.ToArray();

            // 🚨 IMPORTANT 🚨 runtime generated rigs don't expect dynamic generation?
            // So tell the rig it needs to recalculate stuff 😉
            rigBuilder.Build();
        }

        [ContextMenu("Reset")]
        private void ClearPossiblePreviousBodySegments()
        {
            foreach (var bodySegment in _sandwormBody)
            {
                if (Application.isEditor) DestroyImmediate(bodySegment);
                else Destroy(bodySegment);
            }

            foreach (var dampedTransform in _dampedTransforms)
            {
                if (Application.isEditor) DestroyImmediate(dampedTransform);
                else Destroy(dampedTransform);
            }
        }

        private void SpawnBodySegments(int length)
        {
            if (bodyPrefab == null || origin == null) return;
            _sandwormBody = new GameObject[length];

            var instantiated = 0;
            while (instantiated < length)
            {
                var bodyParent = instantiated == 0 ? origin : _sandwormBody[instantiated - 1].transform;
                var bodySegment = Instantiate(bodyPrefab, bodyParent);

                bodySegment.name = $"Sandworm Body Segment {instantiated} in {bodyParent.gameObject}";
                bodySegment.transform.position = bodyParent.position + Vector3.back * distanceBetween;
                bodySegment.tag = bodySegmentTag;

                _sandwormBody[instantiated] = bodySegment;
                instantiated++;
            }
        }

        private void AddBodySegmentsToBoneRenderer(int length)
        {
            var newTransforms = new Transform[length];
            var index = 0;

            foreach (var sandwormBodySegment in _sandwormBody)
                newTransforms[index++] = sandwormBodySegment.transform;
        }
    }
}