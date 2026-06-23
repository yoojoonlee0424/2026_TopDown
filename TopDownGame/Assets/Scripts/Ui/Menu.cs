using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool isMenuOn =false;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject GameOvermenuPanel;
    public Health_Controll health_Controll;

    private void Start()
    {
        Invoke("findPlayerRef", 1f);
        GameOvermenuPanel.SetActive(false);
    }



    void Update()
    {
        if(isMenuOn)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
        if(health_Controll != null)
        {
            if (health_Controll.dead)
            {
                GameOverMenu();
            }
        }
 
    }


    public void OpenMenu()
    {
        menuPanel.SetActive(true);

        Time.timeScale = 0f;

    }

    // 메뉴 닫기 (이어하기 버튼 등에 연결 가능)
    public void CloseMenu()
    {
        menuPanel.SetActive(false);

        Time.timeScale = 1f;

    }

    public void GoTotile()
    {
        GameManager.Instance.GoTitle();
    }

    private void GameOverMenu()
    {
        GameOvermenuPanel.SetActive(true);
    }

    private void findPlayerRef()
    {
        health_Controll = GameObject.FindWithTag("Player").GetComponent<Health_Controll>();
    }

    private void OnMenu()
    {
        isMenuOn = !isMenuOn;
    }

    public void isMenuOff()
    {
        isMenuOn = false;   
    }
}
