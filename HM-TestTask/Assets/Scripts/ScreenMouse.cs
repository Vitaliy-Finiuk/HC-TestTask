using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMouse : MonoBehaviour
{

    private Vector3 mousePos;
    public static ScreenMouse instance;
    private Camera mainCamera;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }
    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            Debug.LogError("No Main Camera");
    }
    void Update()
    {
        Vector3 input = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(input);
    }
    public Vector3 GetMousePos()
    {
        return mousePos;
    }
}
