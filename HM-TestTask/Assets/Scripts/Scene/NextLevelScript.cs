using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public int levelLoad = 4;      // поки що 4(це сцена з проходженням лвл-ла)
    public SaveLVL save;
    public blackSkreen black;
    public bool t1 = false;
    public float timeT1;

    void Update()
    {
        if(t1)
        {
            timeT1 += Time.deltaTime;
            if(timeT1 > 0.4)
            {
                save.SaveIdLVL();
                SceneManager.LoadScene(levelLoad);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Hero_Player"))
        {
            black.colorSpeed = 2.2f;
            t1 = true;
            black.faiding = false;
            black.Faid();
        }
    }
}
