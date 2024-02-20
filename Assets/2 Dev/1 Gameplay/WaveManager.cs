using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct EnemiesToSpawn
    {
        public int quantity;
        public GameObject enemy;
    }
    
    [System.Serializable]
    public class WaveContent
    {
        public float waveDuration;
        
        [SerializeField] [NonReorderable] private EnemiesToSpawn[] enemiesToSpawn;
        
        public EnemiesToSpawn[] GetEnemiesSpawnList()
        {
            return enemiesToSpawn;
        }
    }

    [SerializeField] [NonReorderable] private WaveContent[] waves;

    public int currentWave = -1;

    private void Start()
    {
        StartCoroutine(WaveCooldown());
    }

    void SpawnWave()
    {
        int currentEnemy = 0;
        {
            for (int i = 0; i < waves[currentWave].GetEnemiesSpawnList().Length; i++)
            {
                for (int j = 0; j < waves[currentWave].GetEnemiesSpawnList()[currentEnemy].quantity; j++)
                {
                    Debug.Log("Enemy Spawn : " + waves[currentWave].GetEnemiesSpawnList()[i].enemy);
                }

                currentEnemy++;
            }

            if (currentWave < waves.Length - 1)
                StartCoroutine(WaveCooldown());
        }
    }

    IEnumerator WaveCooldown()
    {
        currentWave++;
        yield return new WaitForSeconds(waves[currentWave].waveDuration);
        SpawnWave();
    }
}
