using DG.Tweening;
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

    private int currentWave = 0;
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

    private void SpawnWave()
    {
        var wave = waves[currentWave];
        var enemyWave = wave.GetEnemiesSpawnList();

        if (enemyWave.Length > 0)
        {
            var spawnSequences = new Sequence[enemyWave.Length];
            float interval;
            EnemyType enemyType;

            for (var i = 0; i < enemyWave.Length; i++)
                if (enemyWave[i].quantity > 0)
                {
                    spawnSequences[i] = DOTween.Sequence();
                    interval = enemyWave[i].duration / enemyWave[i].quantity;
                    enemyType = enemyWave[i].enemyType;
                    for (var j = 0; j < enemyWave[i].quantity; j++)
                    {
                        spawnSequences[i].AppendCallback(() => Enemy.Spawn(enemyType));
                        spawnSequences[i].AppendInterval(interval);
                    }
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