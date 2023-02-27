using JordanTama.UI.Core;

namespace JordanTama.UI.Generic
{
    public class CloseDialogue : DialogueComponent<Dialogue>
    {
        public virtual void Close() => Service.Pop();
        
        protected override void Subscribe() { }
        
        protected override void Unsubscribe() { }
    }
}
