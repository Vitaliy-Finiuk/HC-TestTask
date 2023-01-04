using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour
{
    public float posx;
    public float posy;
    public float posz;
    void Update()
    {

        Vector3 mousePosition = ScreenMouse.instance.GetMousePos();
        posx = mousePosition.x;
        posy = mousePosition.y;
        posz = 1;

        transform.position = new Vector3(posx, posy, posz);
    }
}
