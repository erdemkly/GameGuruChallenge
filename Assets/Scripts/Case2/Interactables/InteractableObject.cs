using System;
using UnityEngine;
namespace Case2.Interactables
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public bool IsActive { get; set; }
        public abstract void OnInteractBegin();
        public abstract void OnInteractStay();
        public abstract void OnInteractExit();

        private void OnEnable()
        {
            IsActive = true;
        }
    }
}
