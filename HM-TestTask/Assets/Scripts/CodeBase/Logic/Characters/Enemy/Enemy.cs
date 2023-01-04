using System;
using CodeBase.Logic.Characters.Hero;
using CodeBase.Logic.Characters.NoiseController;
using CodeBase.Logic.Weapons.Melee;
using CodeBase.Logic.Weapons.Shootable;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace CodeBase.Logic.Characters.Enemy
{
    public enum EnemyStates
    {
        Patrol,
        Wander,
    }
    
    public class Enemy : Unit
    {
        private EnemyStates actualState;
        
        [Header("Patrol")]
        public Transform[] points;
        private int destenationPoint = 0;
        public float minRemainingDistance = 1.5f;
        public float patrolSpeed = 5;

        [Header("Targeting")]
        public GameObject target;
        public string tagOfTarget = "Hero_Player";
        public float timeToDisappear = 2f;
        public float timeToRunWhenHearShoot = 3f;
        [SerializeField] private bool FindTargetAfterSpawn;

        [Header("NavMesh")]
        public float ifCantSeeDistance = 2;
        public float ifCanSeeDistance = 6;
        public float standartSpeed = 14;
        public float randomWaypointAccuracy = 1.0f;

        [Header("Waypoints Settings")]
        public PrWaypointsRoute waypointRoute;
        private int actualWaypoint = 0;
        [HideInInspector]
        public Transform[] waypoints;
        public bool waypointPingPong = false;
        private bool inverseDirection = false; 
        private bool waiting = false;
        private float timeToWait = 3.0f;
        private float actualTimeToWait = 0.0f;
        private float waitTimer = 0.0f;
        public Vector3 finalGoal = Vector3.zero;
        
        [Header("Visibility")]
        public float radius = 5f;
        [Range(1, 360)] public float angle = 45f;
        public LayerMask targetLayer;
        public LayerMask obstructionlayer;

        [Header("Bash")]
        public float bushTime = 2;
        private SpriteRenderer spriteR;
        public Sprite bodyBush;
        private Sprite body;
        public GameObject tapki;
        public bool complex = false;

        [Header("CurrentWeapon")]
        public GameObject CurrentWeapon;
        public bool weaponSeenInIdle = true;

        [Header("WeaponDrop")]
        public GameObject weapon;

        public bool CanSeePlayer { get => canSeePlayer; }
        private bool canSeePlayer;

        private HeroController heroTarget;

        private Shooting shooting;
        private MeleeWeapon melee;
        private NavMeshAgent agent;

        private Vector3 lastDirection;

        private float appearTimer = 0;
        private bool bush = false;
        public GameObject[] deadBody;
        public bool meele;
        private float startRotation;
        private Rigidbody2D rg;
        public Animator animator;

        public AudioClip bush_clip;
        private AudioSource audioSource;
        
    
        public override void Awake()
        {    
            audioSource = GetComponent<AudioSource>();   
            rg = GetComponent<Rigidbody2D>();
            startRotation = rg.rotation;
            if (complex)
            {
                spriteR = this.GetComponent<SpriteRenderer>();
                body = spriteR.sprite;
            }

            if (weaponSeenInIdle)
            {
                CurrentWeapon.SetActive(false);
                animator.SetBool("weapon", false);
            }
            base.Awake();
            onDeath.AddListener(Death);

            if(!meele)
            {
                shooting = GetComponent<Shooting>();
                shooting.useExternalInput = true;
            }
            else
            {
                melee = GetComponent<MeleeWeapon>();
                melee.useExternalInput1 = false;
            }
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            IsEnemy = true;

            if (!target)
            {
                target = GameObject.FindGameObjectWithTag(tagOfTarget);
                heroTarget = target.GetComponent<HeroController>();

                if (!target) Debug.LogError("Dont forget about target");
            }

            if (!target && FindTargetAfterSpawn)
            {
                target = GameObject.FindGameObjectWithTag(tagOfTarget);
                heroTarget = target.GetComponent<HeroController>();

                if (!target) Debug.LogError("Dont forget about target");
            }

            onTrigger.AddListener(TriggerByShoot);
        }

        private void Start()
        {
            waypointRoute = FindObjectOfType<PrWaypointsRoute>();
            
            if (waypoints.Length > 0)
            {
                finalGoal = waypoints[0].transform.position;
            }
        }

        private void FixedUpdate()
        {
            FindingFOV();
            Targetering(Time.fixedDeltaTime);
        }

        public void TriggerByShoot() => 
            appearTimer = timeToRunWhenHearShoot;

        public void Death(Unit unit)
        {
            animator.SetBool("move", false);
            if (NoiseUnitManager.instance.isAvailable)
            {
                NoiseUnitManager.instance.OnDeath(unit);
            }
            //
            GameObject corp = Instantiate(deadBody[Random.Range(0, deadBody.Length)], transform.position, transform.rotation);
            //
            if(weapon != null)
            {
                GameObject dropWep = Instantiate(weapon, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        
        public void SetWaypoints()
        {
            if (waypointRoute)
            {
                waypoints = new Transform[waypointRoute.waypoints.Length];
                timeToWait = waypointRoute.timeToWait;

                for (int i = 0; i < (waypoints.Length); i++)
                {
                    waypoints[i] = waypointRoute.waypoints[i];

                }
            }
            else
            {
                if (actualState == EnemyStates.Patrol)
                {
                    actualState = EnemyStates.Wander;
                }
            }
        }
        
        
        void SetRandomPosVar(Vector3 goal)
        {
            finalGoal = goal + new Vector3(Random.Range(-randomWaypointAccuracy, randomWaypointAccuracy), 0, Random.Range(-randomWaypointAccuracy, randomWaypointAccuracy));
        }

        void SetTimeToWait()
        {
            actualTimeToWait = Random.Range(timeToWait * 0.75f, -timeToWait * 0.75f) + timeToWait;
        }
        

        public void Patrol()
        {
            agent.stoppingDistance = 1;
            agent.speed = patrolSpeed;
       
            if ((points.Length == 0) || (canSeePlayer == true) )
            {
                // Debug.Log("where the points?"); 
                points = waypointRoute.waypoints;
                return;
            }      
        
            if (!(!agent.pathPending && agent.remainingDistance < minRemainingDistance))
            {
                Vector2 Direction = agent.velocity;
                transform.up = Direction;
            }
               
            agent.SetDestination(points[destenationPoint].position);
            animator.SetBool("move", true);
            if (!agent.pathPending && agent.remainingDistance < minRemainingDistance)
            {
                destenationPoint = (destenationPoint + 1) % points.Length;
                if (points.Length == 1)
                {
                    rg.rotation = startRotation;
                    CurrentWeapon.SetActive(false);
                    animator.SetBool("move", false);
                    animator.SetBool("weapon", false);
                }
            }
        }
        public void FindingFOV()
        {
            Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

            if (rangeCheck.Length > 0)
            {
                Transform target = rangeCheck[0].transform;
                Vector2 directionToTarget = (target.position - transform.position).normalized;

                if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    if (Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionlayer))
                        canSeePlayer = false;
                    else
                    {
                        canSeePlayer = true;
                        if (!bush)
                        {
                            animator.enabled = true;
                            appearTimer = timeToDisappear;
                            CurrentWeapon.SetActive(true);
                            animator.SetBool("weapon", true);
                        }
                    }
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }

        private void ApplyState()
        {
            switch (actualState)
            {
                case EnemyStates.Patrol:

                    if (waypoints.Length > 0)
                    {
                        if (agent.remainingDistance >= 1.0f && !waiting)
                        {
                        }
                        else if (!waiting && waitTimer < actualTimeToWait)
                        {
                            waiting = true;
                        }

                        if (waiting)
                        {
                            if (waitTimer < actualTimeToWait)
                                waitTimer += Time.deltaTime;
                            else
                                ChangeWaytpoint();
                        }
                    }

                    //Debug.Log("patrolling");
                    break;
            }
        }

        void ChangeWaytpoint()
            {
                waiting = false;
                if (!waypointPingPong) //Unidirectional Waypoint
                {
                    if (actualWaypoint < waypoints.Length - 1)
                        actualWaypoint = actualWaypoint + 1;
                    else
                        actualWaypoint = 0;
                }
                else if (waypointPingPong)//Ping pong waypoints
                {
                    if (!inverseDirection)
                    {
                        if (actualWaypoint < waypoints.Length - 1)
                            actualWaypoint = actualWaypoint + 1;
                        else
                        {
                            inverseDirection = true;
                            actualWaypoint = actualWaypoint - 1;
                        }
                    }
                    else
                    {
                        if (actualWaypoint > 0)
                            actualWaypoint = actualWaypoint - 1;
                        else
                        {
                            inverseDirection = false;
                            actualWaypoint = 1;
                        }
                    }
                }
                waitTimer = 0.0f;
                SetTimeToWait();
                SetWaypoint(waypoints[actualWaypoint].transform.position);

            }

            public void SetWaypoint(Vector3 Pos)
            {
                SetRandomPosVar(Pos);
                agent.SetDestination(finalGoal);
            }
            
            public void GetCLoserWaypoint()
            {
        
                int selected = 0;
                float selDist = 999f;
                float dist = 0.0f;
                bool changeWayp = false;
                if (waypoints.Length > 0)
                {
                    for (int i = 0; i < waypoints.Length; i++)
                    {
                        dist = agent.remainingDistance;

                        if (dist <= selDist)
                        {
                            selDist = dist;

                            actualWaypoint = selected;

                            changeWayp = true;
                        }
                        selected += 1;
                    }
                    if (changeWayp)
                    {
                        ChangeWaytpoint();
                    }
                    else
                    {
                        SetWaypoint(waypoints[0].position);

                    }
                }
            }

        
        public void Targetering(float dt)
        {
            appearTimer = Mathf.Max(appearTimer - dt, 0);
            if (!bush)
            {
                animator.enabled = true;
                agent.enabled = true;

                if (canSeePlayer && !meele)
                {
                    shooting.Shoot();
                    agent.stoppingDistance = ifCanSeeDistance;

                    Vector2 targetPos = target.transform.position - transform.position;

                }
                else if (canSeePlayer && meele)
                {
                    melee.Attack();
                    agent.stoppingDistance = 2;

                    Vector2 targetPos = target.transform.position - transform.position;

                }
                else
                    agent.stoppingDistance = ifCantSeeDistance;

                if (appearTimer > 0)
                {
                    agent.enabled = true;
                    agent.speed = standartSpeed;
                    if (canSeePlayer)
                    {
                        Vector2 targetPos = target.transform.position - transform.position;
                        transform.up = targetPos;

                    }
                    else
                    {
                        Vector2 Direction = agent.velocity;
                        transform.up = Direction;
                    }

                    lastDirection = transform.up;
                    animator.SetBool("move", true);
                    agent.SetDestination(target.transform.position);
                }
                else
                    Patrol();
            }
            else
            {
                agent.enabled = false;
                if (appearTimer == 0)
                {
                    bush = false;
                    agent.speed = standartSpeed;

                    if(complex) 
                        spriteR.sprite = body;
                }
            }
        }

        public void Bush()
        {
            animator.enabled = false;
            bush = true;
            agent.speed = 0;
            appearTimer = bushTime;
            audioSource.PlayOneShot(bush_clip);
            if(complex)
            {
                transform.up = -agent.velocity;
                spriteR.sprite = bodyBush;
                tapki.gameObject.SetActive(false);
            }
        }





#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

            Vector3 angle01 = Utils.Utils.DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
            Vector3 angle02 = Utils.Utils.DirectionFromAngle(-transform.eulerAngles.z, angle / 2);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
            Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

            if (CanSeePlayer && Application.isPlaying)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, target.transform.position);
            }
        }


#endif

    }
}
