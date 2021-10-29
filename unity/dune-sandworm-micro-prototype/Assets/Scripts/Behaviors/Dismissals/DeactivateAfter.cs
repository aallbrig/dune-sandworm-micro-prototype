using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Behaviors.Dismissals
{
    public class DeactivateAfter : MonoBehaviour, IDismissableBehavior
    {
        [SerializeField] private UserInterfaceConfiguration uiConfig;
        private IEnumerator _coroutine;
        private bool _dismissed = false;

        public bool CanDismiss => !_dismissed;

        public void Dismiss()
        {
            _dismissed = true;
            _coroutine = DeactivateCoroutine(uiConfig.tipDismissTime);
            StartCoroutine(_coroutine);
        }

        public void Reset()
        {
            _dismissed = false;
            gameObject.SetActive(true);
        }

        private IEnumerator DeactivateCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            gameObject.SetActive(false);
        }
    }
}