using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Shooting
{
    public class GunController : MonoBehaviour
    {
        //나중에 SO로 관리할 것
        [Header("무기 관련")]
        [SerializeField] private float cooldown = 0.25f;
        [SerializeField] private float reloadTime = 4f;
        private float cooldownTimer;
        private float reloadTimer;

        [Header("Ref")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField]private Animator muzzleFlashAnimator;

        [Header("Ammo")]
        [SerializeField] private int initialAmmo;
        [SerializeField] private int clipSize;

        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);
        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

        private bool isShooting = false;
        private bool isAiming = false;
        private bool isReloading = false;

        private void Awake()
        {
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
            cooldownTimer += Time.deltaTime;
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
            if (cooldownTimer < cooldown)
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
            cooldownTimer = 0;  
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



        }

        #region Input
        private void OnShoot(InputValue value)
        {
            /*if(value.Get<float>() > 0.1f)
                Shoot();*/

            isShooting = true;
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
