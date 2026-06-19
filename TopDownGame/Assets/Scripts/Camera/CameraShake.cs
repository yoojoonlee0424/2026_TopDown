using System.Collections;
using TopDown.Shooting;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]private CinemachineImpulseSource Shoot_impulseSource;
    [SerializeField]private CinemachineImpulseSource Melee_impulseSource;
    private GunController _gunController;
    private bool isShoot;
    private bool isAim;

    private void Awake()
    {
        _gunController = FindAnyObjectByType<GunController>();
    }

    private void Update()
    {
        if(isShoot&&isAim)
        {
            ShootCam();
        }
        else if(isShoot)
        {
            MeleeCam();
        }
    }

    private void ShootCam()
    {
        if(_gunController.isReloading)
        {
            return;
        }
        StartCoroutine(ShootcamShake());
    }

    private void MeleeCam()
    {
        if (_gunController.isReloading)
        {
            return;
        }
        if(isAim)
        {
            return;
        }
        StartCoroutine(MeleecamShake());
    }

    private IEnumerator ShootcamShake()
    {
        yield return new WaitForSeconds(0.1f);

        Shoot_impulseSource.GenerateImpulse();
    }

    private IEnumerator MeleecamShake()
    {
        yield return new WaitForSeconds(0.1f);

        Melee_impulseSource.GenerateImpulse();
    }

    private void OnShoot()
    {
        isShoot = true;
    }

    private void OnShootRelease()
    {
        isShoot = false;
    }

    private void OnAim()
    {
        isAim = true;
    }

    private void OnAimRelease()
    {
        isAim = false;
    }
}
