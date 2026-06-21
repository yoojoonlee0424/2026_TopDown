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

    public bool UseItem()
    {
        if(statToChange == StatToChange.Health)
        {
            Health_Controll health_Controll = GameObject.Find("Player").GetComponent<Health_Controll>();
            if(health_Controll.currentHealth == health_Controll.startingHealth)
            {
                return false;
            }
            else
            {
                health_Controll.AddHealth(amountToChangeStat);
                return true;
            }
        }
        
        if (statToChange == StatToChange.Ammo)
        {
            GunController gunController = GameObject.Find("Player").GetComponent<GunController>();
            if(gunController.TotalAmmo.Value == 150)
            {
                return false;
            }
            else
            {
                gunController.AddAmmo(amountToChangeStat);
                return true;
            }
        }

        if (statToChange == StatToChange.stamina)
        {
            Stamina stamina = GameObject.Find("Player").GetComponent<Stamina>();
            if(stamina.currentStamina == stamina.MaxStamina)
            {
                return false;
            }
            else
            {
                stamina.AddStamina(amountToChangeStat);
                return true;
            }

        }

        return false;
    }


    public enum StatToChange
    {
        None,
        Health,
        Ammo,
        stamina
    };

}
