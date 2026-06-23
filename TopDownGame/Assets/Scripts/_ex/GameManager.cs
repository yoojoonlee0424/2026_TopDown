using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string titleSceneName = "TitleScene";
    public string gameSceneName = "GameScene";
    public string gameSceneName2 = "GameScene";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void StartLoopGame()
    {
        SceneManager.LoadScene(gameSceneName2);
    }

    public void GameOver()
    {
        NewGameDataManager.Instance.SaveGameResult(MapSeedSaver.Instance._seed);
        GoTitle();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }


}
