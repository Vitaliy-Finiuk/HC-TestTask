using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLVL : MonoBehaviour
{
    public HeroController HR;
    public WeaponManager WM;
    public int level;
    public int weapon;
    public int[] ammunition = new int[2];


    void Start()
    {
        level = HR.levelToLoad;
    }

    void Update()
    {
        PlayerPrefs.SetInt("lvlLOAD", level);
        level = PlayerPrefs.GetInt("lvlLOAD");
    }
    public void AllValue()
    {
        weapon = WM.currentSelectedWeaponID;           // зброя яка зберігається


        if(weapon != -1 && !WM.weaponSlots[weapon].cold)               // перевірка чи зброя холодна
        {
            var weaponO = WM.GetCurrentWeapon();       // інфа про САМЕ ту зброю, яка в руках (патрони і тд)
            ammunition[0] = weaponO.ammo;              // патрони
            ammunition[1] = weaponO.nowAmmo;           // патрони
        }

    }

    public void SaveIdLVL()
    {
        AllValue();
        PlayerPrefs.SetInt("weaponLOAD", weapon);
        if(weapon != -1 && !WM.weaponSlots[weapon].cold)
        {
            PlayerPrefs.SetInt("ammoLOAD", ammunition[0]);
            PlayerPrefs.SetInt("nowAmmoLOAD", ammunition[1]);
        }
        PlayerPrefs.Save();
    }
}
