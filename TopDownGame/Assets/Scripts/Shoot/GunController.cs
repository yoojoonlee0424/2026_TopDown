using TopDown.Movement;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Shooting
{
    public class GunController : MonoBehaviour
    {
        //나중에 SO로 관리할 것
        [Header("공격 관련")]
        [SerializeField] private float FireCooldown = 0.25f;
        [SerializeField] private float reloadTime = 4f;
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

        [Header("Ammo")]
        [SerializeField] private int initialAmmo;
        [SerializeField] private int clipSize;

        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);
        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

        private bool isShooting = false;
        public bool isAiming = false;
        private bool isReloading = false;

        private Stamina stamina;

        private void Awake()
        {
            stamina = GetComponent<Stamina>();

            TotalAmmo.Value = initialAmmo;

            if(initialAmmo <= clipSize)
            {
                CurrentAmmoInClip.Value = initialAmmo;
            }
            else
            {
                CurrentAmmoInClip.Value = clipSize;
            }
        }

        private void Update()
        {
            FireCooldownTimer += Time.deltaTime;
            meleeCooldownTimer += Time.deltaTime;
            reloadTimer += Time.deltaTime;

            if(reloadTimer > reloadTime)
            {
                isReloading = false;
            }

            if (isShooting && isAiming)
            {
                Shoot();
            }

        }

        private void Shoot()
        {
            if (FireCooldownTimer < FireCooldown)
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

            GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation,null);
            bullet.GetComponent<Projectile>().ShootBullet(firePoint);

            muzzleFlashAnimator.SetTrigger("Shoot");

            Debug.Log("Shoot!");
            FireCooldownTimer = 0;  
            CurrentAmmoInClip.Value --;
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
            missingAmmo = clipSize - CurrentAmmoInClip.Value;

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

            GameObject melee = Instantiate(meleePrefab, firePoint.position, firePoint.rotation, null);
            meleeCooldownTimer = 0;

            stamina.StaminaCost(meleeStaminaCost);

            Debug.Log("Melee!");
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
