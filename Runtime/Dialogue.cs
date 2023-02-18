using System.Collections;
using JordanTama.ServiceLocator;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Core
{
    /// <summary>
    /// A Dialogue is a collection of <see cref="DialogueComponent{T}"/>s that should be presented and interacted with at the same time.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Dialogue : MonoBehaviour
    {
        public readonly UnityEvent Promoted = new();
        public readonly UnityEvent Demoted = new();
        public readonly UnityEvent Closed = new();
        
        protected UIService Service;
        protected CanvasGroup CanvasGroup;
        
        
        #region MonoBehaviour

        private void Awake()
        {
            Locator.Get(out Service);
            CanvasGroup = GetComponent<CanvasGroup>();
            
            OnAwake();
            
            StartCoroutine(Service.Add(this));
        }

        #endregion
        
        
        #region Dialogue

        internal IEnumerator Promote()
        {
            CanvasGroup.interactable = true;
            Promoted.Invoke();
            yield return StartCoroutine(OnPromoted());
        }

        internal IEnumerator Demote()
        {
            CanvasGroup.interactable = false;
            Demoted.Invoke();
            yield return StartCoroutine(OnDemoted());
        }
        
        public IEnumerator Close()
        {
            Closed.Invoke();
            yield return StartCoroutine(OnClose());
            Destroy(gameObject);
        }

        protected abstract IEnumerator OnPromoted();
        protected abstract IEnumerator OnDemoted();
        protected abstract IEnumerator OnClose();

        protected virtual void OnAwake() {}
        
        #endregion
    }
}
