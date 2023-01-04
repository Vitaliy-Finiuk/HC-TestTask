using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public int levelLoad;
    public blackSkreen black;
    public float timeT1 = 0;
    public bool t1 = false;
    public bool t2 = false;



    void Start()
    {
        if(PlayerPrefs.HasKey("lvlLOAD"))
        {
            levelLoad = PlayerPrefs.GetInt("lvlLOAD");
        }
        if(PlayerPrefs.GetInt("lvlLOAD") == 1)
        {
            PlayerPrefs.SetInt("weaponLOAD", -1);
        }
    }

    void Update()
    {
        if(t1 && !t2)
        {
            timeT1 += Time.deltaTime;
            if(timeT1 > 0.5)
            {
                PlayerPrefs.SetInt("weaponLOAD", -1);
                SceneManager.LoadScene(6); //1
            }
        }
        if(t2 && !t1)
        {
            timeT1 += Time.deltaTime;
            if(timeT1 > 0.5)
            {
                SceneManager.LoadScene(levelLoad);
            }
        }
    }

    public void StartM()
    {
        t1 = true;
        black.faiding = false;
        black.panel.SetActive(true);

        black.Faid();
    }
    
    public void ContinueM()
    {
        t2 = true;
        black.faiding = false;
        black.panel.SetActive(true);

        black.Faid();
    }

    public void ExitM()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

}