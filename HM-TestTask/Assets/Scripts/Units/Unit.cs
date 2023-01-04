using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public int hp;
    public int maxHP = 100;

    [HideInInspector]
    public UnityEvent<Unit> onDeath;

    [HideInInspector]
    public UnityEvent onTrigger;

    [HideInInspector]
    public bool IsEnemy;


    public virtual void Awake()
    {
        hp = maxHP;
    }

    public void Trigger()
    {
        onTrigger?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        hp -= Mathf.Abs(amount);
        hp = Mathf.Clamp(hp, 0, maxHP);

        if (hp <= 0)
        {
            onDeath?.Invoke(this);
        }
    }
}
