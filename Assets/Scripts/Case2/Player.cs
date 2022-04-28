using System;
using Case2.Interactables;
using Case2.Managers;
using UnityEngine;

namespace Case2
{
    public class Player : MonoBehaviour
    {
        public Animator animator;
        public float speed;

        public void Run()
        {
            transform.position += Vector3.forward*speed*Time.deltaTime;
        }

        private void OnEnable()
        {
            GameManager.Instance.player = this;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableObject interactable))
            {
                interactable.OnInteractBegin();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out InteractableObject interactable))
            {
                interactable.OnInteractStay();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out InteractableObject interactable))
            {
                interactable.OnInteractExit();
            }
        }
    }
}
