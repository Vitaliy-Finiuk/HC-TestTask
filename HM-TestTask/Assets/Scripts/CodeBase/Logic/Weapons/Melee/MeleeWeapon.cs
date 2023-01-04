using System;
using CodeBase.Infrastucture;
using CodeBase.Logic.Characters;
using CodeBase.Logic.Characters.Enemy;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Logic.Weapons.Melee
{
    public class MeleeWeapon : MonoBehaviour
    {
        private IInputService IInputService;
        
        public LayerMask attackLayer;
        [Header("Params")]
        public int damage = 50;
        public float angle = 45;
        public float rangeAttack = 2;
        public float fireRate = 60;

        [Header("References")]
        public Animator animator;
        public UnityEvent OnAttack; 

        private float fireRatePerSeconds;
        private float lastShootTime;

        private bool shootInput;
        public bool hasAnimation = false;
        public bool useExternalInput1 = false;
        public bool bushing = false;
        public float timeT1 = 0;

        private void Awake()
        {
            IInputService = Game.InputService;
        }

        private void FixedUpdate()
        {
            fireRatePerSeconds = 1 / (fireRate / 60);

            if (!useExternalInput1) 
                shootInput = IInputService.IsAttackButtonDown();

            if(bushing)
            {
                timeT1 += Time.deltaTime;
                if(timeT1 > 2)
                {
                    timeT1 = 0;
                    bushing = false;
                }
            }


            if (shootInput)
            {
                if (Time.fixedTime > fireRatePerSeconds + lastShootTime)
                {
                    if(hasAnimation)
                    {
                        animator.SetTrigger("attack");
                        OnAttack?.Invoke();
                        Attack();
                        lastShootTime = Time.time;
                    }
                    else
                    {
                        OnAttack?.Invoke();
                        Attack();
                        lastShootTime = Time.time;
                    }
                }
            }

        }

        public void Attack()
        {
            Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, rangeAttack, attackLayer);
       
            if (rangeCheck.Length > 0)
            {
                Transform target = rangeCheck[0].transform;
                Vector2 directionToTarget = (target.position - transform.position).normalized;

                if (Vector2.Angle(transform.right, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, attackLayer);

                    if (hit)
                    {
                        Unit unit = hit.transform.GetComponent<Unit>();
                        Enemy enemySetup = hit.transform.GetComponent<Enemy>();

                        if (unit) 
                            unit.TakeDamage(damage);

                        if(unit)
                        {
                            if(damage == 0 && !bushing)
                            {
                                Debug.Log("Bush without weapon");
                                enemySetup.Bush();
                                bushing = true;
                            }
                            if(damage == 0 && bushing)
                            {
                                if(timeT1 > 0)
                                    unit.TakeDamage(100);
                            }
                        
                        }
                    }
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            Vector3 angle01 = Utils.Utils.DirectionFromAngle(-transform.eulerAngles.z + 90, -angle / 2);
            Vector3 angle02 = Utils.Utils.DirectionFromAngle(-transform.eulerAngles.z + 90, angle / 2);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + angle01 * rangeAttack);
            Gizmos.DrawLine(transform.position, transform.position + angle02 * rangeAttack);

        }


#endif
    }
}
