using UnityEngine;

namespace CodeBase.Logic.Characters
{
    public class EnemyUnit : Unit
    {
        public int Health = 50;
        public int MaxHP = 100;

        public override void Awake()
        {
            Health = MaxHP;
        }
        
        public override void TakeDamage(int amount)
        {
            Health -= Mathf.Abs(amount);
            Health = Mathf.Clamp(Health, 0, MaxHP);
            
            if (Health <= 0)
            {
                onDeath?.Invoke(this);
            }
        }
    }
}