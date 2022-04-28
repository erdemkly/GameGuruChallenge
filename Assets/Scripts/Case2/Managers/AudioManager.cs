using Helper;
using NaughtyAttributes;
using UnityEngine;
namespace Case2.Managers
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private AudioClip successSound;
        [SerializeField] private AudioSource audioSource;

        public void Success(float pitch)
        {
            pitch = Mathf.Clamp(pitch, 0, 3);
            audioSource.pitch = pitch;
            audioSource.Pause();
            audioSource.PlayOneShot(successSound);
        }
        [Button()]
        public void Success()
        {
            audioSource.PlayOneShot(successSound);
        }
    }
}
