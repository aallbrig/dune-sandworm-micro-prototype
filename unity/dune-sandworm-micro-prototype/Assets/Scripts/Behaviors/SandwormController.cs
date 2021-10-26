using Generated;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviors
{
    [RequireComponent(typeof(Sandworm))]
    public class SandwormController : MonoBehaviour
    {
        private PlayerControls _controls;
        private Sandworm _sandworm;

        private void Awake() => _controls = new PlayerControls();

        private void Start()
        {
            _sandworm = GetComponent<Sandworm>();

            _controls.Player.Interact.started += OnInteractStarted;
            _controls.Player.Interact.canceled += OnInteractStopped;
        }

        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void OnInteractStopped(InputAction.CallbackContext context) => Debug.Log("Interact stopped");

        private void OnInteractStarted(InputAction.CallbackContext context) => Debug.Log("Interact started");
    }
}