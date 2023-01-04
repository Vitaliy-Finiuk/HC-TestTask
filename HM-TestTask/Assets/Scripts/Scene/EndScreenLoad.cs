using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndScreenLoad : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI completeText;
    public float timeT1 = 0;
    public float timeT2 = 0;
    public bool crab = false;
    public blackSkreen black;

    void Start()
    {
        level = PlayerPrefs.GetInt("lvlLOAD");
        completeText.text = "Level " + level.ToString() + " Complete";
    }



    void Update()
    {
        timeT1 += Time.deltaTime;
        if(timeT1 > 1)
        {
            if(Input.anyKeyDown)
            {
                crab = true;
                Debug.Log(timeT2);
                black.faiding = false;
                black.Faid();
            }
        }
        if(crab)
        {
            timeT2 += Time.deltaTime;
            if(timeT2 > 0.8)
            {
                level +=1;
                SceneManager.LoadScene(level);
            }
        }
    }
}
