using UnityEngine;

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


        private void Update()
        {
            cooldownTimer += Time.deltaTime;
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
        private void OnShoot()
        {
            Shoot();
        }
        #endregion


    }

}
