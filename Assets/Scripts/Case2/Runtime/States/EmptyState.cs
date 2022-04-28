using UnityEngine;
using UnityEngine.EventSystems;

namespace Case2.Runtime.States
{
    public class EmptyState : State
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
        }
        public override void OnDrag(PointerEventData eventData)
        {
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
        }
        public override void OnStart()
        {
            Debug.Log("Enter EmptyState");
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
}
