using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource audioSource;
    public float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Hero_Player"))
        {
            if(timer > 1.5)
            {
                audioSource.PlayOneShot(sound);
                timer = 0;
            }
        }
    }
}
