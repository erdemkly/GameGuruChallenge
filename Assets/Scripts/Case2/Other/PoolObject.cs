using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Case2.Other
{
    public class PoolObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Renderer r;
        public void SetPoolBlock(Vector3 pos, Vector3 scale, Material mat)
        {
            DOTween.Kill(transform);
            transform.position = pos;
            transform.localScale = scale;
            r.material = mat;
            rb.isKinematic=false;
            DOVirtual.DelayedCall(3,()=>{
                ResetPoolBlock();
            }).SetId(transform);
        }
        public void ResetPoolBlock(){
            if(gameObject == null) return;
            rb.isKinematic=true;
            gameObject.SetActive(false);
        }
    }
}