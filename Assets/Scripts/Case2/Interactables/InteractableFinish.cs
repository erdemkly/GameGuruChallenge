using System.Collections;
using System.Collections.Generic;
using Case2.Managers;
using UnityEngine;

namespace Case2.Interactables
{
    public class InteractableFinish : InteractableObject
    {
        public override void OnInteractBegin()
        {
            if(!IsActive) return;
            IsActive=false;
            ParticleManager.Instance
            .GetMyParticle("StormExplosion")
            .PlayParticle(transform.position);
            GameManager.Instance.WinEventHandle();
            gameObject.SetActive(false);
        }

        public override void OnInteractExit()
        {

        }

        public override void OnInteractStay()
        {

        }
    }
}