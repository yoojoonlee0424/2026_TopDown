using TopDown.Shooting;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]private CinemachineImpulseSource Shoot_impulseSource;
    [SerializeField]private CinemachineImpulseSource Melee_impulseSource;
    [SerializeField]private CinemachineImpulseSource Hit_impulseSource;
    private GunController _gunController;
    private InventoryManager _inventoryManager;
    private Menu _menu;
    private bool isShoot;
    private bool isAim;

    private void Awake()
    {
        _gunController = FindAnyObjectByType<GunController>();
        _inventoryManager = FindAnyObjectByType<InventoryManager>();
        _menu = FindAnyObjectByType<Menu>();
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
        if (_inventoryManager.menuActivated)
        {
            return;
        }
        if (_inventoryManager.menuActivated)
        {
            return;
        }
        if(_menu.isMenuOn)
        {
            return;
        }
        Invoke("ShootcamShake", 0.1f);
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
        if (_inventoryManager.menuActivated)
        {
            return;
        }
        if (_menu.isMenuOn)
        {
            return;
        }
        Invoke("MeleecamShake",0.1f);
    }

    public void HitCam()
    {
        Hit_impulseSource.GenerateImpulse();
    }

    private void ShootcamShake()
    {
        Shoot_impulseSource.GenerateImpulse();
    }

    private void MeleecamShake()
    {
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
