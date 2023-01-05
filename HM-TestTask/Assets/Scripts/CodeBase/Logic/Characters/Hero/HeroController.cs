using CodeBase.Infrastucture;
using CodeBase.Logic.CameraLogic;
using CodeBase.Logic.Characters.NoiseController;
using CodeBase.Logic.Weapons;
using CodeBase.Logic.Weapons.Shootable;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Logic.Characters.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(WeaponManager))]
    public class HeroController : HeroUnit
    {
        private enum States
        {
            Normal,
            Rolling,
        }

        [SerializeField] private States _states;

        [SerializeField] private HeroAnimator _heroAnimator;

        private IInputService _inputService;

        public LayerMask EnemyLayer;
        public UnityEvent hears;

        [SerializeField] private bool _blockRotation = false;
        [SerializeField] private bool _blockMovement = false;
        
        [SerializeField] private float _speed = 12f;
        [SerializeField] private float _rollSpeed = 90f;

        [SerializeField] [Header("Weapons")] private float _pickupRadiusRange = 1f;

        public LayerMask gunsLayer;
        public bool trueShoot = true;
        public GameObject[] deadBody;

        public GameObject panel;
        private Rigidbody2D _rigidbody2D;
        private WeaponManager _weaponManager;


        private float currentSpeed;
        private Vector2 Horizontal;
        private Vector2 Vertical;

        private Vector3 _rollDirection;
        private Vector3 movedirection;

        public override void Awake()
        {
            _inputService = Game.InputService;
            base.Awake();
        }

        private void Start()
        {
            _weaponManager = GetComponent<WeaponManager>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            onDeath.AddListener(Death);

            _states = States.Normal;
        }

        private void Update()
        {
            _heroAnimator.SelectedWeapon();
            
            switch (_states)
            {
                case States.Normal:

                    IteractWithItem();
                    
                    if (_inputService.IsDodgeButtonUp())
                    {
                        _rollDirection = movedirection;
                        _rollSpeed = 90f;
                        _states = States.Rolling;
                    }

                    break;
                case States.Rolling:
                    Rolling();
                    break;
            }
        }

        private void FixedUpdate()
        {
            Horizontal.x = _inputService.Axis.x;
            Vertical.y = _inputService.Axis.y;
            
            switch (_states)
            {
                case States.Normal:
                    Movement();
                    
                    break;
                case States.Rolling:
                    _rigidbody2D.velocity = _rollDirection * _rollSpeed;
                    break;
            }
        }

        private void IteractWithItem()
        {
            if (_inputService.IsIteractButton())
            {
                if (_weaponManager.HasWeapon())
                {
                    _weaponManager.Drop(true);
                    _heroAnimator.DropItem();
                    return;
                }

                RaycastHit2D hit = Physics2D.CircleCast(transform.position, _pickupRadiusRange, transform.right,
                    0f,
                    gunsLayer.value);
                        
                if (hit)
                {
                    WeaponDrop drop = hit.transform.gameObject.GetComponent<WeaponDrop>();
                    _weaponManager.Pickup(drop.id);

                    Shooting weapon = _weaponManager.weaponSlots[drop.id].weaponHolder.GetComponent<Shooting>();
                    if (weapon)
                    {
                        weapon.nowAmmo = drop.nowAmmo;
                        weapon.ammo = drop.ammo;
                    }

                    Destroy(drop.gameObject);
                }
            }
        }
        
        private void Movement()
        {
            if (!_blockRotation)
            {
                var mousePosition = ScreenMouse.Instance.GetMousePos();
                float AngleRad = Mathf.Atan2(mousePosition.y - transform.position.y,
                    mousePosition.x - transform.position.x);
                float AngleDeg = (180 / Mathf.PI) * AngleRad;
                _rigidbody2D.rotation = AngleDeg;
            }

            if (!_blockMovement)
                _rigidbody2D.MovePosition(_rigidbody2D.position +
                                          _inputService.Axis.normalized * (Time.deltaTime * currentSpeed));

            currentSpeed = _speed;

            movedirection = new Vector3(Horizontal.x, Vertical.y).normalized;

        }
        
        private void Rolling()
        {
            float rollMultiplayer = 10f;
            _rollSpeed -= _rollSpeed * rollMultiplayer * Time.deltaTime;

            float rollSpeedMinimum = 30f;

            if (_rollSpeed < rollSpeedMinimum) 
                _states = States.Normal;
        }
        
        public void Death(Unit unit)
        {
            if (NoiseUnitManager.instance.isAvailable) 
                NoiseUnitManager.instance.OnDeath(unit);

            GameObject corp = Instantiate(deadBody[Random.Range(0, deadBody.Length)], transform.position,
                transform.rotation);

            panel.SetActive(true);
            Destroy(gameObject);
        }
    }
}