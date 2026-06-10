using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Game Setting Data")]
public class GameSettingData : ScriptableObject
{
    public int startHp = 100;
    public float attackSpeed = 1f;
    public float moveSpeed = 5f;

    public int hpUpgradeAmount = 1;
    public float atkUpgradeAmount = 1;
}
