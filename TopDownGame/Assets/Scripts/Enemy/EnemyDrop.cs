using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Serializable]
    public struct DropItem
    {
        [Tooltip("아이템 프리팹")]
        public GameObject itemPrefab;

        [Range(0f, 100f)]
        [Tooltip("(0% ~ 100%)")]
        public float dropChance;
    }

    [Header("드랍 설정")]
    [SerializeField] private List<DropItem> dropTable = new List<DropItem>();

    /// <summary>
    /// 적이 사망할 때 호출될 메서드입니다.
    /// </summary>
    public void HandleDrop()
    {
        if (dropTable == null || dropTable.Count == 0) return;

        foreach (var item in dropTable)
        {
            if (item.itemPrefab == null) continue;

            // 0.0에서 100.0 사이의 랜덤 값 계산
            float randomRoll = UnityEngine.Random.Range(0f, 100f);

            // 주사위 굴린 값이 설정한 확률보다 작으면 아이템 생성
            if (randomRoll <= item.dropChance)
            {
                // 적의 현재 위치와 회전값으로 아이템 생성
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log(transform.position + "에 아이템 생성");
            }
        }
    }
}