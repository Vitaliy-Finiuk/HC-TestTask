using UnityEngine;

namespace CodeBase.Services.Input
{
    abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Fire = "Fire1";
        protected const string Use = "Equip Item";
        protected const string Roll = "Roll";

        public abstract Vector2 Axis { get; }
        
        public abstract bool IsAttackButtonDown();


        public abstract bool IsIteractButton();

        public abstract bool IsDodgeButtonUp();
    }
}