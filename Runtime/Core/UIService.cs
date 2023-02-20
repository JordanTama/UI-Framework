using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JordanTama.ServiceLocator;
using UnityEngine.Events;

namespace JordanTama.UI.Core
{
    /// <summary>
    /// A singleton manager responsible for controlling the <see cref="Dialogue"/> 'stack'.
    /// </summary>
    [Serializable]
    public class UIService : IService
    {
        internal readonly UnityEvent<Dialogue> DialogueAdded = new();
        
        private readonly List<Dialogue> _dialogues = new();

        /// <summary>
        /// Add a <see cref="Dialogue"/> to the top of the stack.
        /// </summary>
        /// <param name="dialogue">The <see cref="Dialogue"/> to be added.</param>
        /// <typeparam name="T">The type of <see cref="Dialogue"/>.</typeparam>
        internal IEnumerator Add<T>(T dialogue) where T : Dialogue
        {
            Dialogue front = _dialogues.FirstOrDefault();
            
            _dialogues.Insert(0, dialogue);
            DialogueAdded.Invoke(dialogue);
            
            if (front != null)
                yield return front.StartCoroutine(front.Demote());

            yield return dialogue.StartCoroutine(dialogue.Promote());
        }

        /// <summary>
        /// Remove a <see cref="Dialogue"/> from the stack.
        /// </summary>
        /// <param name="dialogue">The <see cref="Dialogue"/> to remove.</param>
        internal IEnumerator Remove(Dialogue dialogue)
        {
            _dialogues.Remove(dialogue);
            yield return dialogue.Close();
        }

        /// <summary>
        /// Remove the currently active <see cref="Dialogue"/> from the top of the stack.
        /// </summary>
        /// <returns>Returns the <see cref="Dialogue"/> that was removed.</returns>
        public IEnumerator Pop()
        {
            Dialogue removed = Peek();

            if (removed != null)
            {
                _dialogues.Remove(removed);
                yield return removed.StartCoroutine(removed.Close());
            }

            Dialogue front = _dialogues.FirstOrDefault();
            if (front != null)
                yield return front.StartCoroutine(front.Promote());
        }

        /// <summary>
        /// Queries the manager for the currently active <see cref="Dialogue"/> without altering the stack.
        /// </summary>
        /// <returns>Returns the currently active <see cref="Dialogue"/>.</returns>
        public Dialogue Peek() => _dialogues.FirstOrDefault();

        /// <summary>
        /// Queries the manager for the highest instance of a <see cref="Dialogue"/> of type <c>T</c> in the stack.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="Dialogue"/> to search for.</typeparam>
        /// <returns>Returns the first instance of <c>T</c> in the stack, starting from the top.</returns>
        internal T GetDialogue<T>() where T : Dialogue => _dialogues.Find(d => d is T) as T;
        
        public void OnRegistered() { }
        
        public void OnUnregistered() { }
    }
}
