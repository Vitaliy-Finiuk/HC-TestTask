using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public GameObject scope;
    void FixedUpdate()
    {
        Vector2 scopePos = (scope.transform.position - transform.position).normalized;
        transform.up = scopePos;
    }
}
