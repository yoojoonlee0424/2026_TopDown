using TMPro;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    public GameObject gameModPanel;
    public GameObject SetModPanel;
    public GameObject RePanel;

    public TMP_Text Seed_Text;
    public TMP_Text Time_Text;

    public void Start()
    {
        gameModPanel.SetActive(false);
        SetModPanel.SetActive(false);
        RePanel.SetActive(false);
        Debug.Log(NewGameDataManager.Instance.saveData.MapSeed.ToString());
    }

    public void GameStartButton()
    {
        GameManager.Instance.StartGame();
    }

    public void GameStartLoopButton()
    {
        GameManager.Instance.StartLoopGame();
    }

    public void ModPanelOn()
    {
        gameModPanel.SetActive(true);
    }

    public void ModPanelOff()
    {
        gameModPanel.SetActive(false);
    }

    public void isSeedSave(bool isOn)
    {
        MapSeedSaver.Instance.isSave = isOn;
    }

    public void GameOff()
    {
        Application.Quit();
    }

    public void OpenSet()
    {
        SetModPanel.SetActive(true);
    }

    public void CloseSet()
    {
        SetModPanel.SetActive(false);
    }

    public void OpenRe()
    {
        Seed_Text.text = "시드 :" + NewGameDataManager.Instance.saveData.MapSeed.ToString();
        Time_Text.text = "생존 시간 :" + NewGameDataManager.Instance.saveData.Time.ToString() + "초";
        RePanel.SetActive(true);

    }    

    public void CloseRe()
    {
        RePanel.SetActive(false);
    }
}
