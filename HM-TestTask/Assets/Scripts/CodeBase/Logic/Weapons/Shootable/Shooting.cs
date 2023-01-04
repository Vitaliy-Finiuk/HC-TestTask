using System;
using CodeBase.Infrastucture;
using CodeBase.Logic.Characters;
using CodeBase.Logic.Characters.Hero;
using CodeBase.Logic.Characters.NoiseController;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace CodeBase.Logic.Weapons.Shootable
{
    public class Shooting : MonoBehaviour
    {
        private IInputService IInputService;
        
        [Header("Projectile")]
        public Transform firePoint;
        public GameObject projectilePrefab;
        public float projectileForce = 20;
        public UnityEvent onShooting;
        public WeaponManager WM;
        public HeroController HR;

        [Header("Rates")] [SerializeField] private float _fireRate = 600f;
        public bool semiMode = false;

        [Header("Ammo")]
        public bool UseAmmoSystem = false;
        public int ammo;
        public int maxAmmo = 30;
        public int nowAmmo = 30;

        [Header("Reload")]
        public bool useAutoReload = true;
        public float reloadTime = 2f;
        public UnityEvent onReloading;

        [Header("Spraying")]
        public bool useSpraying;
        public float angle = 45f;
        public int bulletPerShot = 5;

        [Header("External")]
        public bool useExternalInput = false;

        private float fireRatePerSeconds;
        private float lastShootTime;

        private float reloadTimer;

        private bool _shootInput;
        private bool _reloadInput;
        private bool _IsReloading;
        private bool HasAmmo => ammo > 0;
        
        public bool SetShootInput
        {
            get => _shootInput;
            set => _shootInput = value;
        }
        public bool SetReloadInput
        {
            get => _reloadInput;
            set => _reloadInput = value;
        }

        private Unit owner;

        private void Awake()
        {
            IInputService = Game.InputService;

            if (WM)
                owner = WM.transform.GetComponent<Unit>();
            else
                owner = transform.GetComponent<Unit>();

        }

        private void Update()
        {
            if (!useExternalInput) 
                _shootInput = IInputService.IsAttackButtonDown();
        }

        private void FixedUpdate()
        {
            fireRatePerSeconds = 1 / (_fireRate / 60);


            if (_shootInput && HR.trueShoot == true) 
                Shoot();

            if (useAutoReload && !HasAmmo) 
                Reload();

            if (_IsReloading)
            {
                reloadTimer += Time.deltaTime;
                Debug.Log(reloadTimer);
                if (reloadTimer > reloadTime)
                {
                    if (nowAmmo > 0)
                    {
                        onReloading?.Invoke();
                        nowAmmo = nowAmmo - (maxAmmo - ammo);
                        ammo = maxAmmo;
                        reloadTimer = 0;
                        _IsReloading = false;
                    }
                    else if (nowAmmo <= 0) 
                        ammo = ammo;
                }
            }
        }

        public void Shoot()
        {
            if (_IsReloading) return;
            if (!HasAmmo) return;


            if (Time.time > fireRatePerSeconds + lastShootTime)
            {
                NoiseUnitManager.instance.Trigger(owner);

                if (useSpraying) ShootMultiple();
                else ShootSingle();

                lastShootTime = Time.time;

                onShooting?.Invoke();

                if (UseAmmoSystem)
                    ammo--;
            }

        }

        private void ShootSingle()
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
        }
        
        private void ShootMultiple()
        {
            for (int i = 0; i < bulletPerShot; i++)
            {
                float spread = Random.Range(-angle, angle);

                Vector2 dir = firePoint.up;

                float angleLook = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

                Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angleLook + spread));

                GameObject bullet = Instantiate(projectilePrefab, firePoint.position, bulletRotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddRelativeForce(Vector2.up * projectileForce, ForceMode2D.Impulse);
            }
        }

        private void Reload()
        {
            if (nowAmmo > 0)
            {
                _IsReloading = true;
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!useSpraying) return;
            Gizmos.color = Color.white;
            Vector3 angle01 = Utils.Utils.DirectionFromAngle(-transform.eulerAngles.z + 90, -angle);
            Vector3 angle02 = Utils.Utils.DirectionFromAngle(-transform.eulerAngles.z + 90, angle);
        
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + angle01 * 5f);
            Gizmos.DrawLine(transform.position, transform.position + angle02 * 5f);
        }
#endif

    }
}