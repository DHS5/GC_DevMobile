using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Registration

    private static List<Enemy> _enemies = new();
    private static bool _hasEnemy = false;

    public static void Register(Enemy enemy)
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
            OnEnemiesChange();
        }
    }
    public static void Unregister(Enemy enemy)
    {
        if (_enemies.Remove(enemy))
            OnEnemiesChange();
    }

    private static void OnEnemiesChange()
    {
        if (!_hasEnemy && _enemies.Count > 0)
        {
            OnStartUpdate();
        }
        else if (_hasEnemy && _enemies.Count == 0)
        {
            OnEndUpdate();
        }
    }

    #endregion

    #region Update

    private static void OnStartUpdate()
    {
        UpdateManager.OnUpdate += OnUpdate;
    }
    private static void OnEndUpdate()
    {
        UpdateManager.OnUpdate -= OnUpdate;
    }

    private static void OnUpdate(int frameIndex, float deltaTime, float time)
    {
        foreach (var enemy in _enemies)
        {
            enemy.OnUpdate(deltaTime, time);
        }
    }

    #endregion
}
