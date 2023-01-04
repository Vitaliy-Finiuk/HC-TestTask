using Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.CameraLogic
{
    public class ScreenControll : MonoBehaviour
    {
        public int levelLoad;
        public BlackScreen black;
        public float timeT1 = 0;
        public bool t1 = false;

        void Start()
        {
            if(PlayerPrefs.GetInt("lvlLOAD") == 1)
            {
                PlayerPrefs.SetInt("weaponLOAD", -1);
            }
        }

        void Update()
        {
            if(t1)
            {
                timeT1 += Time.deltaTime;
                if(timeT1 > 0.5)
                {
                    PlayerPrefs.SetInt("weaponLOAD", -1);
                    SceneManager.LoadScene(1);
                }
            }
            if(Input.anyKeyDown)
            {
                t1 = true;
                black.faiding = false;
                black.panel.SetActive(true);

                black.Faid();
            }
        }
    }
}
