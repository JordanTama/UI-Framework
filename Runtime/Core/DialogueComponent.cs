using System.Linq;
using JordanTama.ServiceLocator;
using UnityEngine;

namespace JordanTama.UI.Core
{
    /// <summary>
    /// A UIComponent is any UI element that exists as part of a <see cref="Core.Dialogue"/>. All UI Elements should extend this class. 
    /// </summary>
    /// <typeparam name="T">The <see cref="Core.Dialogue"/> type this UIComponent is a part of.</typeparam>
    public abstract class DialogueComponent<T> : MonoBehaviour where T : Dialogue
    {
        protected UIService Service;
        protected T Dialogue;
        
        #region MonoBehaviour
        
        private void Awake()
        {
            Locator.Get(out Service);
            OnComponentAwake();
        }

        private void OnEnable()
        {
            Service.DialogueAdded.AddListener(OnDialogueAdded);
            OnComponentEnabled();
        }

        private void OnDisable()
        {
            OnComponentDisabled();
            Unsubscribe();
            
            Dialogue = null;
        }

        private void Start()
        {
            OnComponentStart();
        }

        #endregion
        
        #region UIComponent
        
        private void OnDialogueAdded(Dialogue addedDialogue)
        {
            if (addedDialogue is not T compatible)
                return;

            Service.DialogueAdded.RemoveListener(OnDialogueAdded);
            Dialogue = compatible;

            Subscribe();
        }
        
        protected virtual void OnComponentAwake() {}

        protected virtual void OnComponentEnabled() {}

        protected virtual void OnComponentDisabled() {}

        protected virtual void OnComponentStart() {}

        protected abstract void Subscribe();

        protected abstract void Unsubscribe();
        
        #endregion
    }
}
