using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject mouseObject;
    public GameObject camera;
    public GameObject blackScreen;
    public float cameraLimitFromHero = 10f;

    public float dampX = 0.2f;
    public float dampY = 0.2f;
    private float velocityX = 0f;
    private float velocityY = 0f;

    void Awake()
    {
        blackScreen.SetActive(true);
    }

    void Update()
    {
        Vector3 target = new Vector3()
        {
            x = mouseObject.transform.position.x,
            y = mouseObject.transform.position.y,
            z = mouseObject.transform.position.z - 10,
        };

        float posX = Mathf.SmoothDamp(camera.transform.position.x, target.x, ref velocityX, dampX);
        float posY = Mathf.SmoothDamp(camera.transform.position.y, target.y, ref velocityY, dampY);

        camera.transform.position = new Vector3(posX, posY, target.z);
    }
}