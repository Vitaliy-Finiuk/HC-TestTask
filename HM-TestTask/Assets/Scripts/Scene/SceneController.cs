using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController instance;


        public void LoadScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }

        private void Awake()
        {
            instance = this;
        }

    }
}
