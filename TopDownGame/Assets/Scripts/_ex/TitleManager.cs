using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject gameModPanel;
    public GameObject SetModPanel;

    public void Start()
    {
        gameModPanel.SetActive(false);
        SetModPanel.SetActive(false);
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

}
