using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bush_Enemy : MonoBehaviour
{
    public LayerMask bushMask = 8;
    public LayerMask floorMask = 15;

    private Rigidbody2D rigidbody2D;
    bool inFloor = false; 

    private void Start()
    {
        inFloor = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rigidbody2D.velocity.magnitude < 1){
            inFloor = true;
        }

        if (inFloor)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            gameObject.layer = Utils.layermaskToLayer(floorMask);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.layer = Utils.layermaskToLayer(bushMask);

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (collision.gameObject.CompareTag("AI"))
        {
            if (!inFloor)
            {
                if (enemy)
                {
                    enemy.Bush();
                }
                inFloor = true;
            }
        }

    }
}