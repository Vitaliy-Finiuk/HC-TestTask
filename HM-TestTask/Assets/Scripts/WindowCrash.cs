using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowCrash : MonoBehaviour
{
    public Sprite crackedGlass;
    private SpriteRenderer spriteR;
    private bool isCracked = false;
    private void Start()
    {
        spriteR = this.GetComponent<SpriteRenderer>();                
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        
        if ((collision.CompareTag("Bullet") || collision.CompareTag("EmemyBullet") && isCracked == false))
        {           
            spriteR.sprite = crackedGlass;
            isCracked = true;
        }
    }
}
