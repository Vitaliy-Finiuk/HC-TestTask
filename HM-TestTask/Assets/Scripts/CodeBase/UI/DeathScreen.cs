using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class DeathScreen : MonoBehaviour
    {
        public float colorSpeed = 1f;
        public float maximumSaturation = 0.2f;
        public Image m_Sprite;
        public int lvl = 0;

        private void Update()
        {
            var color = m_Sprite.color;

            if(gameObject.activeSelf)
            {
                if(color.a <= maximumSaturation)
                {
                    color.a += colorSpeed * Time.deltaTime;
                    m_Sprite.color = color;
                }
                if(Input.anyKeyDown)
                {
                    SceneController.instance.LoadScene(lvl);
                }
            }
        }
    }
}
