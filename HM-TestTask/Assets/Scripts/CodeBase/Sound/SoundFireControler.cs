using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFireControler : MonoBehaviour
{
    public AudioClip reloadClip;
    public AudioClip fireClip;
    private ColdWeapon coldWeapon;
    private Shooting weaponShoot;
    private AudioSource audioSource;
        
    void Start()
    {
        coldWeapon = GetComponent<ColdWeapon>();
        weaponShoot = GetComponent<Shooting>(); 
        audioSource = GetComponent<AudioSource>();
        if (coldWeapon)
        {
            coldWeapon.OnAttack.AddListener(OnAttack);
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
