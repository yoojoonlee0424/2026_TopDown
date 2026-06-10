using System.IO;
using UnityEngine;

public class NewGameDataManager : MonoBehaviour
{
    public static NewGameDataManager Instance;
    public GameSettingData gameSettingData;
    public SaveData saveData;
    public int isTutorialCompleted;

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/saveData.json";

            LoadJsonData();
            LoadPlayerPrefs();
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public int GetPlayerHP()
    {
        int baseHp = gameSettingData.startHp;
        int bonusHp = gameSettingData.hpUpgradeAmount;

        return baseHp + bonusHp * saveData.deathCount;
    }

    public float GetPlayerAttackSpeed()
    {
        float baseAtkSpeed = gameSettingData.attackSpeed;
        float bonusAtkSpeed = gameSettingData.atkUpgradeAmount;

        return baseAtkSpeed + bonusAtkSpeed * saveData.deathCount;
    }

    public float GetPlayerMoveSpeed()
    {
        return gameSettingData.moveSpeed;
    }

    public void SaveGameResult()
    {
        saveData.deathCount++;

        SaveJsonData();
    }

    public void SaveJsonData()
    {
        string json = JsonUtility.ToJson(saveData,true);
        File.WriteAllText(savePath, json);

        Debug.Log("JSON data saved to: " + savePath);
    }

    public void LoadJsonData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("JSON data loaded from: " + savePath);
        }
        else
        {
            Debug.LogWarning("No save file found at: " + savePath);
            saveData = new SaveData(); // 기본값으로 초기화
            SaveJsonData(); // 새 파일 생성
        }
    }

    public void DeleteJsonData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted: " + savePath);
        }
        
        saveData = new SaveData(); // 기본값으로 초기화
        SaveJsonData(); // 새 파일 생성

        Debug.Log("JSON data deleted and new file created at: " + savePath);
    }

    public void LoadPlayerPrefs()
    {
        isTutorialCompleted = PlayerPrefs.GetInt("TUTORIAL", 0);
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("TUTORIAL", isTutorialCompleted);
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs saved: TUTORIAL = " + isTutorialCompleted);
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("TUTORIAL");
        LoadPlayerPrefs(); // 삭제 후 다시 로드하여 기본값으로 초기화

        Debug.Log("PlayerPrefs deleted: TUTORIAL key removed");
    }
}
