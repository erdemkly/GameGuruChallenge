using Case2.Managers;
using UnityEngine;
namespace Case2.Interactables
{
    public class InteractableStar : InteractableObject
    {
        public override void OnInteractBegin()
        {
            if (!IsActive) return;
            IsActive = false;
            gameObject.SetActive(false);
            ParticleManager.Instance.GetMyParticle("StarExplosion").PlayParticle(transform.position);
        }
        public override void OnInteractStay()
        {
            if (!IsActive) return;
        }
        public override void OnInteractExit()
        {
            if (!IsActive) return;
        }
    }
}
