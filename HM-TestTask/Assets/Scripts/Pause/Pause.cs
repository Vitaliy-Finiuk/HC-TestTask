using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject panel;
    public GameObject obj;
    public GameObject cursor;
    public GameObject dScreen;

    void Awake()
    {
        dScreen.SetActive(false);
        panel.SetActive(true);
        panel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !dScreen.activeSelf)
        {

            if(obj.activeSelf)
            {
                panel.SetActive(true);
                obj.SetActive(false);

                cursor.SetActive(false);
                Time.timeScale = 0;
            }
            else if(!obj.activeSelf)
            {
                obj.SetActive(true);
                panel.SetActive(false);

                cursor.SetActive(true);
                Time.timeScale = 1f;
            }
        }
    }
}
