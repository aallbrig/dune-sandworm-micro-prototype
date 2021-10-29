using System.Collections.Generic;
using System.Linq;
using Generated;
using UnityEngine;

namespace Behaviors
{
    public interface IDismissableBehavior
    {
        public bool CanDismiss { get; }
        public void Dismiss();
        public void Reset();
    }

    public class DismissGameplayTip : MonoBehaviour
    {
        [SerializeField] private int dismissAfterInteractionCount = 3;
        private PlayerControls _controls;
        private List<IDismissableBehavior> _dismissables;
        private int _interactionCounter = 0;
        private void Awake() => _controls = new PlayerControls();
        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void Start()
        {
            _dismissables = GetComponents<IDismissableBehavior>().ToList();

            _controls.Player.Move.performed += _ => HandleInteraction();
            _controls.Player.Interact.performed += _ => HandleInteraction();
        }

        private void HandleInteraction()
        {
            _interactionCounter++;
            if (_interactionCounter > dismissAfterInteractionCount) Dismiss();
        }

        private void Dismiss()
        {
            foreach (var dismissable in _dismissables)
            {
                if (dismissable.CanDismiss) dismissable.Dismiss();
            }
        }

        private void Reset()
        {
            _interactionCounter = 0;

            foreach (var dismissable in _dismissables)
            {
                if (dismissable.CanDismiss) dismissable.Reset();
            }
        }
    }
}