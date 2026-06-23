using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject gameModPanel;

    public void Start()
    {
        gameModPanel.SetActive(false);
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

}
