using TopDown.Movement;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace TopDown.Shooting
{
    public class GunController : MonoBehaviour
    {
        //나중에 SO로 관리할 것
        public GunSO gunSO;
        [Header("공격 관련")]
        [SerializeField] private float MeleeCooldown = 2f;
        [SerializeField] private float meleeStaminaCost = 300;

        private float FireCooldownTimer;
        private float reloadTimer;
        private float meleeCooldownTimer;

        [Header("Ref")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject meleePrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Animator muzzleFlashAnimator;
        [SerializeField] private Light2D muzzleFlash;
        [SerializeField] private Light2D gunLight;
        [SerializeField] private Light2D bodyLight;

        [Header("Ammo")]
        public int initialAmmo;

        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);
        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

        private bool isShooting = false;
        public bool isAiming = false;
        public bool isReloading = false;
        public bool isSwap = false;

        private Stamina stamina;
        private InventoryManager _inventoryManager;
        private Menu _menu;

        private void Awake()
        {
            stamina = GetComponent<Stamina>();
            _inventoryManager = FindAnyObjectByType<InventoryManager>();
            _menu = FindAnyObjectByType<Menu>();

            muzzleFlashOff();

            TotalAmmo.Value = initialAmmo;

            if(initialAmmo <= gunSO.clipSize)
            {
                CurrentAmmoInClip.Value = initialAmmo;
            }
            else
            {
                CurrentAmmoInClip.Value = gunSO.clipSize;
            }
        }

        private void Update()
        {
            FireCooldownTimer += Time.deltaTime;
            meleeCooldownTimer += Time.deltaTime;
            reloadTimer += Time.deltaTime;

            gunLightOff();

            if(isSwap)
            {
                if (initialAmmo <= gunSO.clipSize)
                {
                    CurrentAmmoInClip.Value = initialAmmo;
                }
                else
                {
                    CurrentAmmoInClip.Value = gunSO.clipSize;
                }
                isSwap = false;
            }

            if(reloadTimer > gunSO.reloadTime)
            {
                isReloading = false;
            }

            if (isShooting && isAiming)
            {
                Shoot();
            }

            if(isAiming)
            {
                gunLightOn();
            }

        }

        private void Shoot()
        {
            if (FireCooldownTimer < gunSO.FireCooldown)
            {
                return;
            }
            if(CurrentAmmoInClip.Value <= 0)
            {
                return;
            }
            if(isReloading)
            {
                return;
            }
            if(isSwap)
            {
                return;
            }

            GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation,null);
            bullet.GetComponent<Projectile>().ShootBullet(firePoint);

            muzzleFlashOn();

            muzzleFlashAnimator.SetTrigger("Shoot");

            Debug.Log("Shoot!");
            FireCooldownTimer = 0;  
            CurrentAmmoInClip.Value --;

            Invoke("muzzleFlashOff", 0.05f);
        }

        private void Reload()
        {

            reloadTimer = 0;
            isReloading = true;

            if(TotalAmmo.Value <= 0)
            {
                return;
            }

            int missingAmmo;
            missingAmmo = gunSO.clipSize - CurrentAmmoInClip.Value;

            if(missingAmmo == 0)
            {
                return;
            }

            int reloadAmmo;

            if(TotalAmmo.Value >= missingAmmo)
            {
                reloadAmmo = missingAmmo;
            }
            else
            {
                reloadAmmo = TotalAmmo.Value;
            }

            CurrentAmmoInClip.Value += reloadAmmo;
            TotalAmmo.Value -= reloadAmmo;
        }



        private void Melee()
        {
            if (isAiming)
            {
                return;
            }
            if (isReloading)
            {
                return;
            }
            if(meleeCooldownTimer < MeleeCooldown)
            {
                return;
            }
            if(stamina.currentStamina <= meleeStaminaCost)
            {
                return;
            }
            if (_inventoryManager.menuActivated)
            {
                return;
            }
            if (_menu.isMenuOn)
            {
                return;
            }

            GameObject melee = Instantiate(meleePrefab, firePoint.position, firePoint.rotation, null);
            meleeCooldownTimer = 0;

            stamina.StaminaCost(meleeStaminaCost);

            Debug.Log("Melee!");
        }

        public void AddAmmo(int Amount)
        {
            TotalAmmo.Value += Amount;
            if(TotalAmmo.Value >= 150)
            {
                TotalAmmo.Value = 150;
            }
        }


        private void muzzleFlashOn()
        {
            muzzleFlash.enabled = true;
        }

        private void muzzleFlashOff()
        {
            muzzleFlash.enabled = false;
        }

        private void gunLightOn()
        {
            gunLight.enabled = true;
            bodyLight.enabled = false;
        }
        private void gunLightOff()
        {
            gunLight.enabled = false;
            bodyLight.enabled = true;
        }

        #region Input
        private void OnShoot(InputValue value)
        {
            /*if(value.Get<float>() > 0.1f)
                Shoot();*/

            isShooting = true;

            if(!isAiming)
            {
                Melee();
            }
        }

        private void OnShootRelease(InputValue value)
        {
            // 발사 버튼을 놓았을 때의 로직 추가 가능

            isShooting = false;
        }

        private void OnReload()
        {
            // 재장전 관련 로직 추가 가능
            Reload();
        }

        private void OnAim()
        {
            // 조준 관련 로직 추가 가능
            isAiming = true;
        }

        private void OnAimRelease()
        {
            isAiming = false;
        }
        #endregion


    }

}
