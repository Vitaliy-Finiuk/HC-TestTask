using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSaveLVL : MonoBehaviour
{
    public int levelLoad;

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

    void OnMouseDown()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(levelLoad);
        }
    }
}
