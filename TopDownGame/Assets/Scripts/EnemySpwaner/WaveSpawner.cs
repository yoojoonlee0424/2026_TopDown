using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Sequences")]
    [SerializeField] private List<WaveData> waves; // 진행할 웨이브 데이터 리스트
    [SerializeField] private float timeBetweenWaves = 5f; // 웨이브 사이의 휴식 시간

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints; // 적이 스폰될 위치들

    private int currentWaveIndex = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        if (waves == null || waves.Count == 0)
        {
            Debug.LogWarning("Wave Spawner: Date Null");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Wave Spawner: Point Null");
            return;
        }

        // 유니티 6의 비동기 루프 시작
        StartWaveSystemAsync();
    }

    private async void StartWaveSystemAsync()
    {
        while (currentWaveIndex < waves.Count)
        {
            WaveData currentWave = waves[currentWaveIndex];
            Debug.Log($"[Wave {currentWaveIndex + 1}]준비 중 {currentWave.delayBeforeWave}초 후 시작");

            // 웨이브 시작 전 딜레이 (Unity 6 내장 Awaitable 사용)
            await Awaitable.WaitForSecondsAsync(currentWave.delayBeforeWave);

            Debug.Log($"[Wave {currentWaveIndex + 1}] 시작");

            // 적 소환 비동기 메서드 완료 시까지 대기
            await SpawnWaveAsync(currentWave);

            // 현재 웨이브의 모든 적이 죽을 때까지 대기
            while (activeEnemies.Count > 0)
            {
                // 리스트 내에서 이미 파괴(Destroy)되어 null이 된 적 제거
                activeEnemies.RemoveAll(enemy => enemy == null);

                // 프레임 낭비를 방지하기 위해 다음 프레임까지 대기
                await Awaitable.NextFrameAsync();
            }

            Debug.Log($"[Wave {currentWaveIndex + 1}] 클리어");
            currentWaveIndex++;

            // 다음 웨이브가 남아있다면 휴식 시간 부여
            if (currentWaveIndex < waves.Count)
            {
                Debug.Log($"다음 웨이브까지 {timeBetweenWaves}초");
                await Awaitable.WaitForSecondsAsync(timeBetweenWaves);
            }
        }

        Debug.Log("모든 웨이브 클리어");
    }

    private async Awaitable SpawnWaveAsync(WaveData wave)
    {
        foreach (var spawnInfo in wave.enemiesToSpawn)
        {
            if (spawnInfo.enemyPrefab == null) continue;

            for (int i = 0; i < spawnInfo.count; i++)
            {
                SpawnEnemy(spawnInfo.enemyPrefab);

                // 적 소환 간격 대기
                await Awaitable.WaitForSecondsAsync(wave.spawnInterval);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // 랜덤한 스폰 포인트 선택
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject spawnedEnemy = Instantiate(enemyPrefab, randomPoint.position, randomPoint.rotation);
        activeEnemies.Add(spawnedEnemy);
    }
}