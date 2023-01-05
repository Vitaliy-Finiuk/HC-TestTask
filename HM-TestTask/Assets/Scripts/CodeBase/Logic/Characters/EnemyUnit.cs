using UnityEngine;

namespace CodeBase.Logic.Characters
{
    public class EnemyUnit : Unit
    {
        public int hp;
        public int maxHP = 100;

        public override void Awake()
        {
            hp = maxHP;
        }
        
        public override void TakeDamage(int amount)
        {
            hp -= Mathf.Abs(amount);
            hp = Mathf.Clamp(hp, 0, maxHP);
            
            if (hp <= 0)
            {
                onDeath?.Invoke(this);
            }
        }
    }
}