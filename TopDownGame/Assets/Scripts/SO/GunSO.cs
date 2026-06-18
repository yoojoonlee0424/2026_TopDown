using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "Scriptable Objects/GunSO")]
public class GunSO : ScriptableObject
{
    public float FireCooldown = 0.25f;
    public float reloadTime = 4f;

    [Header("Ammo")]
    public int clipSize;
}
