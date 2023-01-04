using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HeroController : Unit
{
    public enum States
    {
        Normal,
        Rolling,
    }
    
    [Header("SoundBlast")]
    public float loudness = 200f;
    public LayerMask EnemyLayer;
    public UnityEvent hears;
    
    public bool blockRotation = false;
    public bool blockMovement = false;
    public float speed = 8f;
    public float runSpeed = 11.5f;
    public float rollSpeed;

    [Header("Weapons")]
    public float pickupRadiusRange = 5f;
    public LayerMask gunsLayer;
    public bool trueShoot = true;
    public GameObject[] deadBody;


    public Animator animator;
    public GameObject panel;
    public int levelToLoad;
    private Rigidbody2D rb;
    private WeaponManager weaponManager;



    private float currentSpeed;


    public States states;
    
    private Vector3 _rollDirection;
    private Vector3 movedirection;

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        rb = GetComponent<Rigidbody2D>();
        onDeath.AddListener(Death);

        states = States.Normal;
    }
    private void Update()
    {
        if ((weaponManager.currentSelectedWeaponID != -1) && (weaponManager.currentSelectedWeaponID != 2) && (weaponManager.currentSelectedWeaponID != 3))
        {
            animator.SetBool("weapon", true);
        }
        else
        {
            animator.SetBool("weapon", false);
        }

        switch (states)
        {
            case States.Normal:

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (weaponManager.HasWeapon())
                    {
                        weaponManager.Drop(true);
                        animator.SetTrigger("drop");
                        return;
                    }

                    RaycastHit2D hit = Physics2D.CircleCast(transform.position, pickupRadiusRange, transform.right, 0f,
                        gunsLayer.value);
                    if (hit)
                    {
                        WeaponDrop drop = hit.transform.gameObject.GetComponent<WeaponDrop>();
                        weaponManager.Pickup(drop.id);

                        Shooting weapon = weaponManager.weaponSlots[drop.id].weaponHolder.GetComponent<Shooting>();
                        if (weapon)
                        {
                            weapon.nowAmmo = drop.nowAmmo;
                            weapon.ammo = drop.ammo;
                        }

                        Destroy(drop.gameObject);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _rollDirection = movedirection;
                    rollSpeed = 90f;
                    states = States.Rolling;
                }
                break;
            case States.Rolling:
                float rollMult = 5f;
                rollSpeed -= rollSpeed * rollMult * Time.deltaTime;

                float rollSpeedMinimum = 50f;

                if (rollSpeed < rollSpeedMinimum)
                {
                    states = States.Normal;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (states)
        {
            case States.Normal:

                if (!blockRotation)
                {
                    var mousePosition = ScreenMouse.instance.GetMousePos();
                    float AngleRad = Mathf.Atan2(mousePosition.y - transform.position.y,
                        mousePosition.x - transform.position.x);
                    float AngleDeg = (180 / Mathf.PI) * AngleRad;
                    rb.rotation = AngleDeg;
                }

                Vector2 input;
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");


                if (!blockMovement)
                {
                    rb.MovePosition(rb.position + input.normalized * Time.deltaTime * currentSpeed);
                }

                if (Input.GetButton("Run"))
                {
                    currentSpeed = runSpeed;
                    animator.speed = 2;
                }
                else
                {
                    currentSpeed = speed;
                    animator.speed = 1;
                }

                if (input.magnitude > 0)
                {
                    animator.SetBool("move", true);
                }
                else
                {
                    animator.SetBool("move", false);
                }

                movedirection = new Vector3(input.x, input.y).normalized;

                
                break;
            case States.Rolling:
                rb.velocity = _rollDirection * rollSpeed;
                break;
        }
    }

    public void Death(Unit unit)
    {
        if (NoiseUnitManager.instance.isAvailable)
        {
            NoiseUnitManager.instance.OnDeath(unit);
        }
        GameObject corp = Instantiate(deadBody[Random.Range(0, deadBody.Length)], transform.position, transform.rotation);
        
        panel.SetActive(true);
        Destroy(gameObject);
    }
}
