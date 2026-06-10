using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Shooting
{
    public class GunController : MonoBehaviour
    {
        [Header("사격 간격")]
        [SerializeField] private float cooldown = 0.25f;
        private float cooldownTimer;

        [Header("Ref")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField]private Animator muzzleFlashAnimator;

        private bool isShooting = false;

        private void Update()
        {
            cooldownTimer += Time.deltaTime;

            if (isShooting)
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

            GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation,null);
            bullet.GetComponent<Projectile>().ShootBullet(firePoint);

            muzzleFlashAnimator.SetTrigger("Shoot");

            Debug.Log("Shoot!");
            cooldownTimer = 0;  
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
        }

        private void OnAim()
        {
            // 조준 관련 로직 추가 가능
        }
        #endregion


    }

}
