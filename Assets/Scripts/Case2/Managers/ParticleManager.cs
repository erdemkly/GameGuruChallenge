using System;
using System.Collections.Generic;
using System.Linq;
using Helper;
using UnityEngine;

namespace Case2.Managers
{
    [Serializable]
    public struct MyParticle
    {
        public int id;
        public string particleName;
        public GameObject particleObject;
        [SerializeField] private bool onFloor;
        [SerializeField]private List<GameObject> pool;
        [SerializeField] private int poolCount;

        public void PlayParticle(Vector3 pos,Transform parent=null)
        {
            if (pool == null) pool = new List<GameObject>();
            var particle = pool.FirstOrDefault(x => !x.activeSelf)?.GetComponent<ParticleSystem>();
            if (particle == null || pool.Count < poolCount)
            {
                particle = GameObject.Instantiate(particleObject, pos, Quaternion.identity, ParticleManager.Instance.transform).GetComponent<ParticleSystem>();
                pool.Add(particle.gameObject);
            }
            particle.gameObject.SetActive(true);
            if (particle == null) return;
            if (parent == null) parent = ParticleManager.Instance.transform;
            if (parent != null) particle.transform.parent = parent;
            if (onFloor) pos.y = 0;
            particle.transform.position = pos;
            particle.Play();
        }
    }
    public class ParticleManager : MonoSingleton<ParticleManager>
    {
        [SerializeField] private List<MyParticle> allMyParticles;

        public MyParticle GetMyParticle(int id)
        {
            return allMyParticles.FirstOrDefault(x => x.id == id);
        }
        public MyParticle GetMyParticle(string particleName)
        {
            return allMyParticles.FirstOrDefault(x => x.particleName == particleName);
        }
    }
}
