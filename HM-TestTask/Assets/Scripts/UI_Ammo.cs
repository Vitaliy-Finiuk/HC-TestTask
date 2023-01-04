using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ammo : MonoBehaviour
{
    public WeaponManager WM;
    public Text textAmmo;

    void Update()
    {
        var weapon = WM.GetCurrentWeapon();
        if(weapon == null)
        {
            textAmmo.text = " ";
        }
        else
        textAmmo.text = weapon.ammo.ToString() + " / " + weapon.nowAmmo.ToString();
    }
}
