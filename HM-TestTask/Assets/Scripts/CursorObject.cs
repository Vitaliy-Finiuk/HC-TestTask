using UnityEngine;

public class CursorObject : MonoBehaviour
{
    private float PositionX;
    private float PositionY;
    private float PositionZ;

    private void Update()
    {
        Vector3 mousePosition = ScreenMouse.instance.GetMousePos();
        
        PositionX = mousePosition.x;
        PositionY = mousePosition.y;
        PositionZ = 1;

        transform.position = new Vector3(PositionX, PositionY, PositionZ);
    }
}
