using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
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
            StaminaBar = GameObject.Find("Stamina_image").GetComponent<Image>();
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

            if (recharge != null)
            {
                StopCoroutine(recharge);
            }
            recharge = StartCoroutine(RechargeStamina());
        }

        private IEnumerator RechargeStamina()
        {
            yield return new WaitForSeconds(rechargeCooldown);

            while ( currentStamina < MaxStamina )
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

        public void AddStamina(float Add)
        {
            currentStamina += Add;
            if(currentStamina >= MaxStamina )
            {
                currentStamina = MaxStamina;
            }
            StaminaBar.fillAmount = currentStamina / MaxStamina;
        }


        /*private void RechargeColor()
        {
            StaminaBar.DOFade(0f, 0.3f).SetLoops(-1, LoopType.Yoyo);
        }*/
    }
}

