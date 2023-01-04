using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSlot
{
    public string name = "AK";
    public GameObject weaponHolder;
    public GameObject pickup;
    public float throwImpulse = 500;
    public bool cold;

    public void SetActive(bool state)
    {
        weaponHolder.SetActive(state);
    }
}

public class WeaponManager : MonoBehaviour
{
    public WeaponSlot[] weaponSlots;

    public int currentSelectedWeaponID;

    void Start()
    {
        currentSelectedWeaponID = PlayerPrefs.GetInt("weaponLOAD");
        if(currentSelectedWeaponID != -1 && !weaponSlots[currentSelectedWeaponID].cold)
        {
            weaponSlots[currentSelectedWeaponID].weaponHolder.GetComponentInChildren<Shooting>().ammo = PlayerPrefs.GetInt("ammoLOAD");
            weaponSlots[currentSelectedWeaponID].weaponHolder.GetComponentInChildren<Shooting>().nowAmmo = PlayerPrefs.GetInt("nowAmmoLOAD");
        }
        weaponSlots[currentSelectedWeaponID].SetActive(true);
    }

    public void Pickup(int id)
    {
        if (currentSelectedWeaponID != -1) { Drop(); }
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (i == id)
            {
                weaponSlots[i].SetActive(true);
                currentSelectedWeaponID = i;
            }
            else
            {
                weaponSlots[i].SetActive(false);
            }
        }
    }

    public Shooting GetCurrentWeapon()
    {
        if (currentSelectedWeaponID == -1)
        {
            return null;
        }
        return weaponSlots[currentSelectedWeaponID].weaponHolder.GetComponentInChildren<Shooting>();
    }

    public void Drop(bool dropWithForce = false)
    {
        if (currentSelectedWeaponID == -1) return;

        for (int i = 0; i < weaponSlots.Length; i++)
        {
            weaponSlots[i].SetActive(false);
        }
        
        GameObject drop = Instantiate(weaponSlots[currentSelectedWeaponID].pickup, transform.position, transform.rotation);

        WeaponDrop dropScript = drop.GetComponent<WeaponDrop>();
        Shooting weapon = weaponSlots[currentSelectedWeaponID].weaponHolder.GetComponent<Shooting>();

        if (weapon)
        {
            dropScript.ammo = weapon.ammo;
            dropScript.nowAmmo = weapon.nowAmmo;
        }

        if (dropWithForce)
        {
            Rigidbody2D body = drop.GetComponent<Rigidbody2D>();
            body.AddForce(transform.right * weaponSlots[currentSelectedWeaponID].throwImpulse * body.mass, ForceMode2D.Impulse);
            body.angularVelocity = 210;
            currentSelectedWeaponID = -1;
        }
    }
    public bool HasWeapon()
    {
        if (currentSelectedWeaponID == -1)
        {
            return false;
        }
        return true;
    }
}
