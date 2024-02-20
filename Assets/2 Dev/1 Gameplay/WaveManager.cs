using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct EnemiesToSpawn
    {
        public float duration;
        public int quantity;
        public EnemyType enemyType;
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

    public int currentWave = 0;
    private Tween _waveSpawnTween;

    private void Start()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        WaveContent wave = waves[currentWave];
        EnemiesToSpawn[] enemyWave = wave.GetEnemiesSpawnList();
        Sequence[] spawnSequences = new Sequence[enemyWave.Length];
        float interval;

        for (int i = 0; i < enemyWave.Length; i++)
        {
            spawnSequences[i] = DOTween.Sequence();
            interval = enemyWave[i].duration / enemyWave[i].quantity;
            for (int j = 0; j < enemyWave[i].quantity; j++)
            {
                spawnSequences[i].AppendCallback(() => Enemy.Spawn(enemyWave[i].enemyType));
                spawnSequences[i].AppendInterval(interval);
            }
        }

        currentWave++;
        if (currentWave < waves.Length)
            _waveSpawnTween = DOVirtual.DelayedCall(wave.waveDuration, SpawnWave);
    }

    public void OnGameOver()
    {
        _waveSpawnTween.Kill();
    }
}
