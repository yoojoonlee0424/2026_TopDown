using UnityEngine;

public class EnemyPlacementStep : GenerationStep
{
    [SerializeField]
    private bool m_applyStep = true;
    /// <summary>
    /// We will use a Random number generator for prefab placement.
    /// </summary>
    private System.Random m_prefabPlacementRandom;

    [SerializeField]
    [Tooltip("생성할 적 수")]
    private int m_enemyCount = 5; 
    private int m_enemyIndex;
    private int m_enemyRandom;

    [SerializeField]
    private GameObject m_enemyPrefab;

    private void Awake()
    {
        m_enemyIndex = Random.Range(1,10) * Random.Range(10,1000);
    }

    /// <summary>
    /// Here we place the player prefab on the map.
    /// </summary>
    /// <param name="generationData"></param>
    public override void Execute(GenerationData generationData)
    {
        if (m_applyStep == false)
            return;

        //To ensure that the placement is repetitive we set the seed of the generator to a known value
        m_prefabPlacementRandom = new System.Random(generationData.MapGenerationSeed);



        for (int i = 0; i < m_enemyCount; i++)
        {
            int randomIndex = m_prefabPlacementRandom.Next(generationData.PossiblePlacementPositions.Count + m_enemyIndex);
            int index = 0;

            //This is how we could randomly place any prefab (not only a player)
            foreach (var position in generationData.PossiblePlacementPositions)
            {
                if (index == randomIndex)
                {
                    GameObject playerReference
                        = Instantiate(m_enemyPrefab, new(position.x, position.y, 0), Quaternion.identity);
                    generationData.PlacedObjects.Add(playerReference);
                    break;
                }
                index++;
            }
        }

    }
}
