using Case2.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Case2.Runtime.States
{
    public class RunState : State
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            BlockManager.Instance.CutBlock();
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
            BlockManager.Instance.NewBlock();
        }
        public override void OnUpdate()
        {
            GameManager.Instance.player.Run();
        }
        public override void OnExit()
        {
        }
    }
}
