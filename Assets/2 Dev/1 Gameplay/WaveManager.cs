using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct EnemiesToSpawn
    {
        public float waveStartTime;
        public float duration;
        public int[] quantities;
        public EnemyType[] enemyTypes;
    }

    [System.Serializable]
    public class WaveContent
    {
        public float waveDelay;

        [SerializeField] [NonReorderable] private EnemiesToSpawn[] enemiesToSpawn;

        public EnemiesToSpawn[] GetEnemiesSpawnList()
        {
            return enemiesToSpawn;
        }
    }

    [SerializeField] private int levelNumber = 3;
    [SerializeField] [NonReorderable] private WaveContent[] waves;

    private int _currentWave = 0;
    private int _currentLevel = 0;
    private Tween _waveSpawnTween;

    private void Start()
    {
        NextWave();
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += OnGameOver;
        EnemyManager.OnAllEnemiesDead += NextWave;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= OnGameOver;
        EnemyManager.OnAllEnemiesDead -= NextWave;
    }

    private void NextWave()
    {        
        //Optimization.GCCollect();
        DOVirtual.DelayedCall(waves[_currentWave].waveDelay / 2, Optimization.GCCollect);
        _waveSpawnTween = DOVirtual.DelayedCall(waves[_currentWave].waveDelay, SpawnWave);
    }

    private List<Sequence> _waveSequences = new();
    private void SpawnWave()
    {
        _waveSequences.Clear();
        var wave = waves[_currentWave];
        var enemyWave = wave.GetEnemiesSpawnList();
        Sequence seq;
        bool lastWave = _currentWave == waves.Length - 1;

        if (enemyWave.Length > 0)
        {
            float interval;

            for (var i = 0; i < enemyWave.Length; i++)
                if (enemyWave[i].quantities[_currentLevel] > 0)
                {
                    seq = DOTween.Sequence();
                    interval = enemyWave[i].duration / enemyWave[i].quantities[_currentLevel];
                    EnemyType enemyType = enemyWave[i].enemyTypes[_currentLevel];
                    if (enemyWave[i].waveStartTime > 0) seq.AppendInterval(enemyWave[i].waveStartTime);
                    for (var j = 0; j < enemyWave[i].quantities[_currentLevel]; j++)
                    {
                        //if (lastWave && i == 0) seq.AppendCallback(() => Enemy.Spawn(enemyType, SpawnWave));
                        seq.AppendCallback(() => Enemy.Spawn(enemyType));
                        seq.AppendInterval(interval);
                    }
                    _waveSequences.Add(seq);
                }
        }

        if (!lastWave)
        {
            _currentWave++;
        }
        else
        {
            _currentWave = 0;
            _currentLevel = Mathf.Min(_currentLevel + 1, levelNumber - 1);
        }
    }

    public void OnGameOver()
    {
        _waveSpawnTween.Kill();
        foreach (var seq in _waveSequences)
        {
            if (seq != null)
                seq.Kill();
        }
    }
}