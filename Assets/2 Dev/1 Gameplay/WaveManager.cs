using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct EnemiesToSpawn
    {
        public float startTime;
        public float duration;
        public int[] quantities;
        public EnemyType[] enemyTypes;
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

    [SerializeField] private int levelNumber = 3;
    [SerializeField] [NonReorderable] private WaveContent[] waves;

    private int _currentWave = 0;
    private int _currentLevel = 0;
    private Tween _waveSpawnTween;

    private void Start()
    {
        SpawnWave();
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= OnGameOver;
    }

    private List<Sequence> _waveSequences = new();
    private void SpawnWave()
    {
        _waveSequences.Clear();
        var wave = waves[_currentWave];
        var enemyWave = wave.GetEnemiesSpawnList();
        Sequence seq;

        if (enemyWave.Length > 0)
        {
            float interval;
            EnemyType enemyType;

            for (var i = 0; i < enemyWave.Length; i++)
                if (enemyWave[i].quantities[_currentLevel] > 0)
                {
                    seq = DOTween.Sequence();
                    interval = enemyWave[i].duration / enemyWave[i].quantities[_currentLevel];
                    enemyType = enemyWave[i].enemyTypes[_currentLevel];
                    if (enemyWave[i].startTime > 0) seq.AppendInterval(enemyWave[i].startTime);
                    for (var j = 0; j < enemyWave[i].quantities[_currentLevel]; j++)
                    {
                        seq.AppendCallback(() => Enemy.Spawn(enemyType));
                        seq.AppendInterval(interval);
                    }
                    _waveSequences.Add(seq);
                }
        }

        _currentWave++;
        if (_currentWave < waves.Length)
        {
            _waveSpawnTween = DOVirtual.DelayedCall(wave.waveDuration, SpawnWave);
        }
        else
        {
            _currentWave = 0;
            _currentLevel = Mathf.Min(_currentLevel + 1, levelNumber - 1);
            _waveSpawnTween = DOVirtual.DelayedCall(wave.waveDuration, SpawnWave);
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