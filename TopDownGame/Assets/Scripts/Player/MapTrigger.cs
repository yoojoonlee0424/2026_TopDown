using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(NewGameDataManager.Instance.isTutorialCompleted == 0)
        {
            Debug.Log("Tutorial not completed, loading tutorial scene.");
            NewGameDataManager.Instance.isTutorialCompleted = 1; // Mark tutorial as completed
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
