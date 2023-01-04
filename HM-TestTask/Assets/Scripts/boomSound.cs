using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomSound : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip boom;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(boom);
        
    }
}
