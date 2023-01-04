using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Hero : MonoBehaviour
{
    public float speed = 8f;
    public float runSpeed = 11.5f;

    public float rotatespeed = 50f;
    public static float vertical, horizontal;
    private Rigidbody2D rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float AngleRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        rb.rotation = AngleDeg;


        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position += new Vector3(0, currentSpeed, 0) * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position += new Vector3(0, -currentSpeed, 0) * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += new Vector3(currentSpeed, 0, 0) * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position += new Vector3(-currentSpeed, 0, 0) * Time.deltaTime;
        //}
        //else { }

        Vector2 input;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + input.normalized * Time.deltaTime * currentSpeed);
        

        if (Input.GetButton("Run"))
        {
            currentSpeed = runSpeed;
        }
        else currentSpeed = speed;

    }
}
