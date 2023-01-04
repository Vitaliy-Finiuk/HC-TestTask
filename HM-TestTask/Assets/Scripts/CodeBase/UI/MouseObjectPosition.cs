using CodeBase.Logic.CameraLogic;
using UnityEngine;

namespace CodeBase.UI
{
    public class MouseObjectPosition : MonoBehaviour
    {
        [SerializeField] private GameObject _hero;
    
        private float PositionX;
        private float PositionY;
        private float PositionZ;

        private void Update()
        {
            Vector3 mousePosition = ScreenMouse.instance.GetMousePos();
        
            var position = _hero.transform.position;
        
            PositionX = (position.x + mousePosition.x)/2;
            PositionY = (position.y + mousePosition.y)/2;
            PositionZ = (position.z + mousePosition.z)/2;

            transform.position = new Vector3(PositionX, PositionY, PositionZ);
        }
    }
}