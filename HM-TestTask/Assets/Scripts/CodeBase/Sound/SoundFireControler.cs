using CodeBase.Logic.Weapons.Melee;
using CodeBase.Logic.Weapons.Shootable;
using UnityEngine;

namespace CodeBase.Sound
{
    public class SoundFireControler : MonoBehaviour
    {
        public AudioClip reloadClip;
        public AudioClip fireClip;
        private MeleeWeapon meleeWeapon;
        private Shooting weaponShoot;
        private AudioSource audioSource;
        
        void Start()
        {
            meleeWeapon = GetComponent<MeleeWeapon>();
            weaponShoot = GetComponent<Shooting>(); 
            audioSource = GetComponent<AudioSource>();
            if (meleeWeapon)
            {
                meleeWeapon.OnAttack.AddListener(OnAttack);
            }  
            if (weaponShoot)
            {
                weaponShoot.onShooting.AddListener(OnAttack);
                weaponShoot.onReloading.AddListener(OnReloading);
            }
        }

        public void OnReloading()
        {
            if (!reloadClip) return;
            audioSource.PlayOneShot(reloadClip);
        }
        public void OnAttack()
        {
            if (!fireClip) return;
            audioSource.PlayOneShot(fireClip);
        }
    }
}
