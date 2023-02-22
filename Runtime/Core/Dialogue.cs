using JordanTama.ServiceLocator;
using UnityEngine;
using UnityEngine.Events;

namespace JordanTama.UI.Core
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
            
            Service.Add(this);
        }

        #endregion
        
        
        #region Dialogue

        internal void Promote()
        {
            OnPromoted();
            Promoted.Invoke();
        }

        internal void Demote()
        {
            OnDemoted();
            Demoted.Invoke();
        }
        
        public void Close()
        {
            OnClose();
            Closed.Invoke();
        }

        protected virtual void OnPromoted() => CanvasGroup.interactable = true;

        protected virtual void OnDemoted() => CanvasGroup.interactable = false;

        protected virtual void OnClose() => Destroy(gameObject);

        protected virtual void OnAwake() {}
        
        #endregion
    }
}
