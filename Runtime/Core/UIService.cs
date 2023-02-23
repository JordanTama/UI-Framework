using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JordanTama.ServiceLocator;
using UnityEngine;
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
        /// <param name="delay">The time to wait between demoting <c>dialogue</c> and promoting the next, in seconds.</param>
        /// <param name="onComplete">Action to invoke once <c>dialogue</c> has been added.</param>
        /// <typeparam name="T">The type of <see cref="Dialogue"/>.</typeparam>
        internal async void Add<T>(T dialogue, float delay = 0f, Action onComplete = null) where T : Dialogue
        {
            Dialogue front = _dialogues.FirstOrDefault();
            if (front != null)
                front.Demote();
            
            _dialogues.Insert(0, dialogue);
            DialogueAdded.Invoke(dialogue);
            
            if (delay > 0f)
            {
                await Task.Delay((int) (delay * 1000f));
                if (!Application.isPlaying)
                    return;
            }

            dialogue.Promote();
            onComplete?.Invoke();
        }

        /// <summary>
        /// Remove a <see cref="Dialogue"/> from the stack.
        /// </summary>
        /// <param name="dialogue">The <see cref="Dialogue"/> to remove.</param>
        internal void Remove(Dialogue dialogue)
        {
            _dialogues.Remove(dialogue);
            dialogue.Close();
        }

        /// <summary>
        /// Remove the currently active <see cref="Dialogue"/> from the top of the stack.
        /// </summary>
        /// <returns>Returns the <see cref="Dialogue"/> that was removed.</returns>
        public async void Pop(float delay = 0f)
        {
            Dialogue removed = Peek();

            if (removed != null)
            {
                _dialogues.Remove(removed);
                removed.Close();
            }

            await Task.Delay((int) (delay * 1000f));

            Dialogue front = _dialogues.FirstOrDefault();
            if (front != null)
                front.Promote();
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
        
        #region IService
        
        public void OnRegistered() { }
        
        public void OnUnregistered() { }
        
        #endregion
    }
}
