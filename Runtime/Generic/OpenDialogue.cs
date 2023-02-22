using JordanTama.UI.Core;
using UnityEngine;

namespace JordanTama.UI.Generic
{
    public class OpenDialogue : DialogueComponent<Dialogue>
    {
        [SerializeField] private GameObject dialoguePrefab;
        [SerializeField] private bool closeThisDialogue;
        [SerializeField] private float delay;
        
        public void Open()
        {
            Dialogue dialogue = Instantiate(dialoguePrefab).GetComponent<Dialogue>();
            Service.Add(dialogue, delay, closeThisDialogue ? () => Service.Pop() : null);
        }

        protected override void Subscribe() { }
        
        protected override void Unsubscribe() { }
    }
}
