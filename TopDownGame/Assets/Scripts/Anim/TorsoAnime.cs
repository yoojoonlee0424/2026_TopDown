using TopDown.Shooting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TorsoAnime : MonoBehaviour
{
    private bool isAiming = false;
    public Animator anim;

    public RuntimeAnimatorController SMGcontroller;
    public RuntimeAnimatorController Pistolcontroller;

    public GunController gunController;

    private void Start()
    {
        anim.enabled = true;
        anim.runtimeAnimatorController = Pistolcontroller;
    }

    private void Update()
    {
        if(isAiming)
        {
            anim.SetBool("IsAim", true);
        }
        else 
        {
            anim.SetBool("IsAim", false);
        }
        if(gunController.isReloading && !isAiming)
        {
            anim.SetBool("IsReload", true);
        }
        else
        {
            anim.SetBool("IsReload", false);
        }

    }

    private void OnShoot(InputValue value)
    {

        if (!isAiming)
        {
            anim.SetTrigger("Melee");
        }
    }


    private void OnAim()
    {
        isAiming = true;

    }

    private void OnAimRelease()
    {
        isAiming = false;

    }

    private void OnWeapon1()
    {
        anim.runtimeAnimatorController = Pistolcontroller;
    }
    private void OnWeapon2()
    {
        anim.runtimeAnimatorController = SMGcontroller;
    }
}
