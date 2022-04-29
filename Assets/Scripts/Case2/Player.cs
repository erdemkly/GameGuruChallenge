using System;
using Case2.Interactables;
using Case2.Managers;
using UnityEngine;

namespace Case2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private BoxCollider boxCollider;
        public Animator animator;
        public Rigidbody rb;
        public float speed;

        public void Run()
        {
            transform.position += Vector3.forward*speed*Time.deltaTime;
        }
        
        public void CheckGround(){
            var ray = Physics.BoxCast(transform.position+Vector3.up*0.61f,boxCollider.bounds.size/2,-transform.up,transform.rotation,boxCollider.bounds.size.y/2,1<<6);
            if(!ray){
                GameManager.Instance.LoseEventHandle();
            }
        }
        private void Start() {
            GameManager.Instance.winEvent.AddListener(()=>animator.SetBool("Dance",true));
            GameManager.Instance.startEvent.AddListener(()=>animator.SetBool("Dance",false));
        }

        public void FallDown(){
            rb.isKinematic = false;
            animator.SetTrigger("Fall");
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
