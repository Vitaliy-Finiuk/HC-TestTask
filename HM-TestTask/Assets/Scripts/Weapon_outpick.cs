using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_outpick : MonoBehaviour
{
    private float id;
    public GameObject Weapon;
    public GameObject Weapon_in;
    private float posx; private float posy; private float posz;


    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetKey(KeyCode.Mouse1) && Weapon.tag == "Weapon")
        {

            Destroy(Weapon);
            Weapon_in.SetActive(true);
        }
    }



    void Start()
    {
        Weapon_in.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(Weapon, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
