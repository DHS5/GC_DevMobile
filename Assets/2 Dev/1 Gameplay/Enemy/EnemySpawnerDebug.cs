using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawnerDebug : MonoBehaviour
{
    [SerializeField] List<EnemyType> enemyTypes;
    [SerializeField] private Vector3 spawnPosition;

    float spawnTimer;
    int enemiesSpawned;

    [SerializeField] int maxEnemies = 10;
    [SerializeField] float spawnInterval = 2f;


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

        enemiesSpawned++;
    }
}
