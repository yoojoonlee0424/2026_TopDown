using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool isMenuOn =false;
    [SerializeField] private GameObject menuPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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



    private void OnMenu()
    {
        isMenuOn = !isMenuOn;
    }
}
