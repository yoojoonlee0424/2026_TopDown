using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthBarImage;
    private Health_Controll health_Controll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health_Controll = GetComponent<Health_Controll>();
        HealthBarImage = GameObject.Find("Health_image").GetComponent<Image>();
    }

    private void Update()
    {
        if (health_Controll != null)
        {
            UpdateHealthBar();
        }
    }


    public void UpdateHealthBar()
    {
        HealthBarImage.fillAmount = health_Controll.currentHealth / health_Controll.startingHealth;
    }
}
