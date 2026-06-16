using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown.Movement
{
    public class Stamina : MonoBehaviour
    {
        public Image StaminaBar;

        public float currentStamina;
        public float MaxStamina;
        public float ChargeRate;

        public float rechargeCooldown;

        private Coroutine recharge;

        private void Awake()
        {
            currentStamina = MaxStamina;
        }

        public void StaminaCost(float cost)
        {
            currentStamina -= cost;
            if(currentStamina < 0 )
            {
                currentStamina = 0;
            }
            StaminaBar.fillAmount = currentStamina / MaxStamina;

            if(recharge != null)
            {
                StopCoroutine(recharge);
            }
            recharge = StartCoroutine(RechargeStamina());
        }

        private IEnumerator RechargeStamina()
        {
            yield return new WaitForSeconds(rechargeCooldown);

            while( currentStamina < MaxStamina )
            {
                currentStamina += ChargeRate / 10f;
                if(currentStamina > MaxStamina)
                {
                    currentStamina = MaxStamina;
                }
                StaminaBar.fillAmount = currentStamina / MaxStamina;
                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}

