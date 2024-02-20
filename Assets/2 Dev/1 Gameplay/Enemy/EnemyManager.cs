using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    #region Registration

    private static List<Enemy> _enemies = new();
    private static bool _hasEnemy = false;

    private static List<Enemy> _toRegister = new();
    private static List<Enemy> _toUnregister = new();

    public static void Register(Enemy enemy)
    {
        _toRegister.Add(enemy);
    }
    public static void Unregister(Enemy enemy)
    {
        _toUnregister.Add(enemy);
    }

    private static void DoRegistrations()
    {
        bool change = false;

        if (_toRegister.IsValid())
        {
            change = true;
            foreach (var e in _toRegister)
            {
                if (!_enemies.Contains(e))
                {
                    _enemies.Add(e);

                }
            }
        }
        if (_toUnregister.IsValid())
        {
            change = true;
            foreach (var b in _toUnregister)
            {
                _enemies.Remove(b);
            }
        }

        if (change)
        {
            OnEnemiesChange();

            _toRegister.Clear();
            _toUnregister.Clear();
        }
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

        DoRegistrations();
    }

    #endregion
}
