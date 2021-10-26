using Generated;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviors
{
    public class Interaction
    {
        public static Interaction Of(Vector2 raw) => new Interaction(raw);
        public Vector2 Position { get; private set; }
        public float Timing { get; private set; }

        private Interaction(Vector2 raw)
        {
            Position = raw;
            Timing = Time.time;
        }
        public override string ToString() => $"Timing: {Timing} Position: {Position}";
    }

    public class Swipe
    {
        private readonly Interaction _start;
        private readonly Interaction _end;
        public float Timing { get; private set; }
        public float Distance { get; private set; }
        public Vector2 Vector { get; private set; }
        public Vector2 VectorNormalized { get; private set; }

        public static Swipe Of(Interaction start, Interaction end) => new Swipe(start, end);
        private Swipe(Interaction start, Interaction end)
        {
            _start = start;
            _end = end;
            CalculateSwipeFacts();
        }

        private void CalculateSwipeFacts()
        {
            Vector = _end.Position - _start.Position;
            VectorNormalized = Vector.normalized;
            Timing = _end.Timing - _start.Timing;
            Distance = Vector2.Distance(_end.Position, _start.Position);
        }

        public override string ToString() => $"Timing: {Timing} Distance: {Distance} Vector: {Vector} Vector Normalized {VectorNormalized}";
    }

    [RequireComponent(typeof(Sandworm))]
    public class SandwormController : MonoBehaviour
    {
        private PlayerControls _controls;
        private Sandworm _sandworm;
        private Interaction _interactionStart;
        private Interaction _interactionEnd;

        private void Awake() => _controls = new PlayerControls();

        private void Start()
        {
            _sandworm = GetComponent<Sandworm>();

            _controls.Player.Interact.started += OnInteractStarted;
            _controls.Player.Interact.canceled += OnInteractStopped;
        }

        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void OnInteractStarted(InputAction.CallbackContext context)
        {
            _interactionStart = Interaction.Of(_controls.Player.Position.ReadValue<Vector2>());
            Debug.Log($"End: {_interactionStart}");
        }

        private void OnInteractStopped(InputAction.CallbackContext context)
        {
            _interactionEnd = Interaction.Of(_controls.Player.Position.ReadValue<Vector2>());
            Debug.Log($"End: {_interactionEnd}");
            var swipe = Swipe.Of(_interactionStart, _interactionEnd);
            Debug.Log($"Swipe: {swipe}");
        }
    }
}