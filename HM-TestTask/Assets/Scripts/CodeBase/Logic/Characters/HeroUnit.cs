using CodeBase.Logic.Characters.Hero;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
    public class HeroUnit : Unit
    {
        private HealthHandler healthHandler;

        public override void Awake()
        {
            healthHandler = GetComponent<HealthHandler>();
        }


        public override void TakeDamage(int amount)
        {
            healthHandler.Health -= amount;
            healthHandler.UpdateHealth();
            

            if (healthHandler.Health <= 0)
            {
                onDeath?.Invoke(this);
            }
        }
    }
}