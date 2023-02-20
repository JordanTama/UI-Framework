using JordanTama.UI.Core;

namespace JordanTama.UI.Generic
{
    public class CloseDialogue : DialogueComponent<Dialogue>
    {
        public void Close()
        {
            if (Service.Peek() == Dialogue)
                StartCoroutine(Service.Pop());
        }
        
        protected override void Subscribe() { }
        
        protected override void Unsubscribe() { }
    }
}
