using JordanTama.UI.Core;
using UnityEngine;

namespace JordanTama.UI.Generic
{
    public class OpenDialogue : DialogueComponent<Dialogue>
    {
        [SerializeField] private Dialogue dialoguePrefab;
        [SerializeField] private bool closeThisDialogue;
        
        public void Open()
        {
            Instantiate(dialoguePrefab);
            
            if (closeThisDialogue)
                StartCoroutine(Service.Pop());
        }

        protected override void Subscribe() { }
        
        protected override void Unsubscribe() { }
    }
}
