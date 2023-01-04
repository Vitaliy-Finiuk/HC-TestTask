using UnityEngine;

namespace CodeBase.Logic.CameraLogic
{
    public class ScreenMouse : MonoBehaviour
    {
        public static ScreenMouse Instance;
        
        private Vector3 mousePos;
        private Camera mainCamera;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
        }
        private void Start()
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
                Debug.LogError("No Main Camera");
        }

        private void Update()
        {
            Vector3 input = Input.mousePosition;
            mousePos = mainCamera.ScreenToWorldPoint(input);
        }
        public Vector3 GetMousePos()
        {
            return mousePos;
        }
    }
}
