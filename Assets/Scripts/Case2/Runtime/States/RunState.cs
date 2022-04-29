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
            GameManager.Instance.player.animator.SetBool("Run",true);
        }
        public override void OnUpdate()
        {
            GameManager.Instance.player.Run();
            GameManager.Instance.player.CheckGround();
        }
        public override void OnExit()
        {
            GameManager.Instance.player.animator.SetBool("Run",false);
        }
    }
}
