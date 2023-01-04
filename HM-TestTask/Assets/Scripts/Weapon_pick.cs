using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_pick : MonoBehaviour
{

    public GameObject Hero;
    public GameObject Weapon;
    public GameObject Weapon_in;
    public float posx;
    public float posy;
    public float posz;

    void Start()
    {
        Weapon_in.SetActive(false);
    }


  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (Weapon.gameObject.CompareTag("Weapon"))
        {
            // Destroy(Weapon.gameObject);
            // Weapon_in.SetActive(true);

        }
    }


    void Update()
    {

    }
}
