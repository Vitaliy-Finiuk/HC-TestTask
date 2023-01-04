using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class load4lvl : MonoBehaviour
{
    public int[] ammunition = new int[2];
    public HeroController HR;
    public WeaponManager WM;
    public int weapon;
    void Start()
    {
        if(weapon != -1 && !WM.weaponSlots[weapon].cold)
        {
            var weaponO = WM.GetCurrentWeapon();
            ammunition[0] = 0;
            ammunition[1] = 0;
            PlayerPrefs.SetInt("ammoLOAD", ammunition[0]);
            PlayerPrefs.SetInt("nowAmmoLOAD", ammunition[1]);
        }
    }
}
