using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyType> enemyTypes;
    [SerializeField] int maxEnemies = 10;
    [SerializeField] float spawnInterval = 2f;

    List<SplineContainer> splines;

    float spawnTimer;
    int enemiesSpawned;

    void OnValidate()
    {
        splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
    }


    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        EnemyType enemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];

        // TODO: Possible optimization - pool enemies
        
        enemiesSpawned++;
    }
}
