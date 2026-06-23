using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public TMP_Text TMP_Text;
    public TMP_Text m_Text;
    public Health_Controll health_Controll;
    public float PlayerTime;
    public float CurrentTime;

    private bool isTimerRunning = true;
    private void Start()
    {
        health_Controll = GameObject.FindWithTag("Player").GetComponent<Health_Controll>();
        PlayerTime = 0;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            PlayerTime += Time.deltaTime;
        }

        if(health_Controll.dead)
        {
            TimerStop();
            TimerSave(PlayerTime);
        }

        TMP_Text.text = "현재 시간" + PlayerTime.ToString() + "초";
        m_Text.text = "생존 시간" + PlayerTime.ToString() + "초";
    }

    public void TimerSave(float Time)
    {
        NewGameDataManager.Instance.SaveGameTime(Time);
    }

    public void TimerReset()
    {
        PlayerTime = 0; 
    }

    public void TimerStop()
    {
        isTimerRunning = false;
    }
}
