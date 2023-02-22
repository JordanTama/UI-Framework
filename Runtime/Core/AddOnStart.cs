using JordanTama.ServiceLocator;
using UnityEngine;

namespace JordanTama.UI.Core
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
