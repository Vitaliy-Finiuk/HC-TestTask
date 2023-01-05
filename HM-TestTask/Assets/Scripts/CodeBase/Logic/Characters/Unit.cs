using System;
using CodeBase.Logic.Characters.Hero;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Logic.Characters
{
    public abstract class Unit : MonoBehaviour
    {

        [HideInInspector]
        public UnityEvent<Unit> onDeath;

        [HideInInspector]
        public UnityEvent onTrigger;

        [HideInInspector]
        public bool IsEnemy;
        
        public virtual void Awake() {}
        
        public void Trigger()
        {
            onTrigger?.Invoke();
        }

        public abstract void TakeDamage(int amount);
    }
}
