using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject glassSplash;
    public GameObject hitBlood;
    public GameObject hitEffect;
    public int damage = 50;
    //public AudioClip fireClip;
    //private AudioSource audioSource;
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       
        Vector2 dir = GetComponent<Rigidbody2D>().velocity.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //audioSource.PlayOneShot(fireClip);
        
        if (collision.CompareTag("AI") || collision.CompareTag("Hero_Player"))
        {
            GameObject effect = Instantiate(hitBlood, transform.position, Quaternion.Euler(0, 0, angle));
        }
        else if (collision.CompareTag("Side"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.Euler(0, 0, angle));           
        }

        if (collision.CompareTag("Glass"))
        {
            GameObject effect = Instantiate(glassSplash, transform.position, Quaternion.Euler(0, 0, angle));
        }
        
            Unit unit = collision.gameObject.GetComponent<Unit>();

        if (unit)
        {
            unit.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

 
}
