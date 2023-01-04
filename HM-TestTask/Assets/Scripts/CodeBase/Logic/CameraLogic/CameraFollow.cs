using UnityEngine;

namespace CodeBase.Logic.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private GameObject _mouseObject;
        [SerializeField] private GameObject camera;
        [SerializeField] private GameObject blackScreen;
        [SerializeField] private float _cameraLimitFromHero = 10f;

        [SerializeField] private float dampX = 0.2f;
        [SerializeField] private float dampY = 0.2f;
    
        private float velocityX = 0f;
        private float velocityY = 0f;

        private void Awake() => 
            blackScreen.SetActive(true);

        private void Update()
        {
            Vector3 target = new Vector3()
            {
                x = _mouseObject.transform.position.x,
                y = _mouseObject.transform.position.y,
                z = _mouseObject.transform.position.z - 10,
            };

            float posX = Mathf.SmoothDamp(camera.transform.position.x, target.x, ref velocityX, dampX);
            float posY = Mathf.SmoothDamp(camera.transform.position.y, target.y, ref velocityY, dampY);

            camera.transform.position = new Vector3(posX, posY, target.z);
        }
    }
}