using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue_p : MonoBehaviour
{
    public GameObject panel;
    public GameObject obj;
    public GameObject cursor;

    public void Continue()
    {
        panel.SetActive(false);
        obj.SetActive(true);
        cursor.SetActive(true);

        Time.timeScale = 1f;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Exit");
    }
}
