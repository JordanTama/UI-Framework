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
        internal readonly UnityEvent Promoted = new();
        internal readonly UnityEvent Demoted = new();
        internal readonly UnityEvent Closed = new();
        
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
            CanvasGroup.interactable = true;
            
            OnPromote();
            Promoted.Invoke();
        }

        internal void Demote()
        {
            CanvasGroup.interactable = false;
            
            OnDemote();
            Demoted.Invoke();
        }
        
        public void Close()
        {
            OnClose();
            Destroy(gameObject);
            Closed.Invoke();
        }

        protected virtual void OnAwake() {}

        protected abstract void OnClose();

        protected abstract void OnPromote();

        protected abstract void OnDemote();
        
        #endregion
    }
}
