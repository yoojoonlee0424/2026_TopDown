using UnityEngine;

public class MapSeedSaver : MonoBehaviour
{
    public static MapSeedSaver Instance;
    public GenerationData _generationData;
    public SaveData saveData;
    public int _seed = 0;
    public bool isSave = false;


    private void Awake()
    {
        if(isSave)
        {
            Invoke("SeedSave", 1f);
            _generationData.RandomizeOffset = true;
        }
        if(!isSave)
        {
            _generationData.RandomizeOffset = false;
        }
    }


    public void SeedSave()
    {
        _seed = _generationData.MapGenerationSeed;
        NewGameDataManager.Instance.SaveGameResult(_seed);
        Debug.LogError(_seed);
    }


}
