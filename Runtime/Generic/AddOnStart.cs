using JordanTama.ServiceLocator;
using JordanTama.UI.Core;
using UnityEngine;

namespace JordanTama.UI.Generic
{
    [RequireComponent(typeof(Dialogue))]
    public class AddOnStart : MonoBehaviour
    {
        private UIService _service;
        private Dialogue _dialogue;
        
        private void Awake()
        {
            Locator.Get(out _service);
            TryGetComponent(out _dialogue);
        }

        private void Start() => _service.Add(_dialogue);
    }
}
