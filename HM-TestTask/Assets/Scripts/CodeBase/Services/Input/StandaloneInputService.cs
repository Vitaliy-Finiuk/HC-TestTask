using UnityEngine;

namespace CodeBase.Services.Input
{
    class StandaloneInputService : InputService
    {

        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = new Vector2(UnityEngine.Input.GetAxisRaw(Horizontal),
                    UnityEngine.Input.GetAxisRaw(Vertical));

                return axis;
            }
        }

        public override bool IsAttackButtonDown() => 
            UnityEngine.Input.GetButton(Fire);

        public override bool IsIteractButton() => 
            UnityEngine.Input.GetButtonDown(Use);

        public override bool IsDodgeButtonUp() => 
            UnityEngine.Input.GetButtonDown(Roll);
    }
}