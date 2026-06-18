using TopDown.Shooting;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private GunSO Weapon1SO;
    [SerializeField] private GunSO Weapon2SO;
    [SerializeField] private float swapTime;

    private GunController gunController;

    private float swapTimer;

    private void Awake()
    {
        gunController = GetComponent<GunController>();
    }

    private void Update()
    {
        swapTime += Time.deltaTime;
    }

    private void OnWeapon1()
    {
        gunController.gunSO = Weapon1SO;
        gunController.isSwap = true;
    }
    private void OnWeapon2()
    {
        gunController.gunSO = Weapon2SO;
        gunController.isSwap = true;
    }
}
