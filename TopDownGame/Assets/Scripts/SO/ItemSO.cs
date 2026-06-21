using System.Collections;
using System.Collections.Generic;
using TopDown.Movement;
using TopDown.Shooting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public void UseItem()
    {
        if(statToChange == StatToChange.Health)
        {
            GameObject.Find("Player").GetComponent<Health_Controll>().AddHealth(amountToChangeStat);
            Debug.LogError("채력추가");
        }

        if (statToChange == StatToChange.Ammo)
        {
            GameObject.Find("Player").GetComponent<GunController>().AddAmmo(amountToChangeStat);
            Debug.LogError("탄약추가");
        }

        if (statToChange == StatToChange.stamina)
        {
            GameObject.Find("Player").GetComponent<Stamina>().AddStamina(amountToChangeStat);
            Debug.LogError("스테미나 버프");
        }
    }


    public enum StatToChange
    {
        None,
        Health,
        Ammo,
        stamina
    };

}
