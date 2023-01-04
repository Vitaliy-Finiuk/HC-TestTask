using UnityEngine;

namespace CodeBase.Sound
{
    public class Music : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip track1;
        public AudioClip track2;
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(track1);
            audioSource.loop = true;
        }
    }
}
