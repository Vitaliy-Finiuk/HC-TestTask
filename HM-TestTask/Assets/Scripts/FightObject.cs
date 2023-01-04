using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightObject : MonoBehaviour
{
    public GameObject hands;
    public WeaponManager WP;
    void Update()
    {
        if(WP.currentSelectedWeaponID == -1)
        {
            hands.SetActive(true);
        }
        else
        {
            hands.SetActive(false);
        }
    }
}
