using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemySpawnInfo
{
    public GameObject enemyPrefab; 
    public int count;              
}

[CreateAssetMenu(fileName = "NewWaveData", menuName = "Wave System/Wave Data")]
public class WaveData : ScriptableObject
{
    [Header("Wave")]
    public List<EnemySpawnInfo> enemiesToSpawn; 
    public float spawnInterval = 1.0f;          
    public float delayBeforeWave = 3.0f;       
}