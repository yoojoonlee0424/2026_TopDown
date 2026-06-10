using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;




[Serializable]
public class PlayerData
{
    public List<string> collectedItems = new List<string>();
    public int stage = 1;
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public PlayerData playerData;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //중복 방지
        }
    }

    public void SaveDate(PlayerData playerData)
    {
        string filePath = Application.persistentDataPath + "/playerData.json";
        string json = JsonUtility.ToJson(playerData, true);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("Data saved to: " + json);
    }

    public PlayerData LoadData()
    {
        string filePath = Application.persistentDataPath + "/player_Data.json";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Data loaded: " + json);
            return playerData;
        }
        else
        {
            Debug.LogWarning("No save file found");
            return new PlayerData(); 
        }
    }

    public void GameStart()
    {
        PlayerData playerData = LoadData();
        if(playerData == null)
        {
            playerData = new PlayerData();
            SceneManager.LoadScene("LV1_1");
        }
        else
        {
            SceneManager.LoadScene("LV" + playerData.stage);
        }
    }

    public void PlayerDead()
    {
        PlayerData playerData = LoadData();
        if(playerData != null)
        {
            playerData.stage = 1;
            
            foreach(string item in playerData.collectedItems.ToList())
            {
                if(UnityEngine.Random.Range(0, 1) == 0) // 50% 확률로 아이템 삭제
                {
                    playerData.collectedItems.Remove(item);
                }
                
            }

            SaveDate(playerData);
        }

        SceneManager.LoadScene("GameOver");

    }




}





