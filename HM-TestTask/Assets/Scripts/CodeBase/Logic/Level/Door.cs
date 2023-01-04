using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class Door : MonoBehaviour
    {
        private const string HeroPlayer = "Hero_Player";

        [SerializeField] private AudioClip _sound;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _timer = 0;

        private void Update() => 
            _timer += Time.deltaTime;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag(HeroPlayer))
            {
                if(_timer > 1.5)
                {
                    _audioSource.PlayOneShot(_sound);
                    _timer = 0;
                }
            }
        }
    }
}
