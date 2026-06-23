using TopDown.Shooting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private GunSO Weapon1SO;
    [SerializeField] private GunSO Weapon2SO;
    [SerializeField] private float swapTime;

    private GunController gunController;
    private Image SMG;
    private Image Pistol;

    private float swapTimer;

    private void Awake()
    {
        gunController = GetComponent<GunController>();
        SMG = GameObject.Find("WeaponIcon SMG").GetComponent<Image>();
        Pistol = GameObject.Find("WeaponIcon Pistol").GetComponent <Image>();
        Pistol.enabled = false;
    }

    private void Update()
    {
        swapTime += Time.deltaTime;
    }

    private void OnWeapon1()
    {
        gunController.gunSO = Weapon1SO;
        gunController.isSwap = true;
        Pistol.enabled = false;
        SMG.enabled = true;
    }
    private void OnWeapon2()
    {
        gunController.gunSO = Weapon2SO;
        gunController.isSwap = true;
        Pistol.enabled = true;
        SMG.enabled = false;   
    }
}
