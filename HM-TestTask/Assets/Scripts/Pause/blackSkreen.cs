using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackSkreen : MonoBehaviour
{
    public Image image;
    public GameObject panel;
    public float colorSpeed = 1f;
    public bool faiding;

    void Awake()
    {
        faiding = true;
    }
    private void Update()
    {
        Faid();
    }


    public void Faid()
    {
        panel.SetActive(true);
        var color = image.color;
        if (faiding)
        {
            if(color.a < 0)
            {
                panel.SetActive(false);
            }
            if (color.a > 0)
            {
                color.a -= colorSpeed * Time.deltaTime;
                image.color = color;
            }
        }
        if (!faiding)
        {
            if (color.a < 1)
            {
                color.a += colorSpeed * Time.deltaTime;
                image.color = color;
            }
        }
    }
}
