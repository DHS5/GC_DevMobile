using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    #region Registration

    public static bool IsActive { get; private set; }

    private static List<Enemy> _enemies = new();
    private static bool _hasEnemy = false;

    private static List<Enemy> _toRegister = new();
    private static List<Enemy> _toUnregister = new();

    public static void Init()
    {
        _toRegister.Clear();
        _toUnregister.Clear();
        _hasEnemy = false;
        _enemies.Clear();
        IsActive = false;
    }

    public static void Register(Enemy enemy)
    {
        if (IsActive)
        {
            _toRegister.Add(enemy);
        }

        else if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
            OnEnemiesChange();
        }
    }

    public static void Unregister(Enemy enemy)
    {
        _toUnregister.Add(enemy);
    }

    private static void DoRegistrations()
    {
        var change = false;

        if (_toRegister.IsValid())
        {
            change = true;
            foreach (var e in _toRegister)
                if (!_enemies.Contains(e))
                    _enemies.Add(e);
        }

        if (_toUnregister.IsValid())
        {
            change = true;
            foreach (var b in _toUnregister) _enemies.Remove(b);
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
            _hasEnemy = true;
            OnStartUpdate();
        }
        else if (_hasEnemy && _enemies.Count == 0)
        {
            _hasEnemy = false;
            OnEndUpdate();
        }
    }

    public static void Clear()
    {
        _toRegister.Clear();
        _toUnregister.Clear();

        foreach (var enemy in _enemies)
            if (enemy.IsActive)
                enemy.Dispose();

        DoRegistrations();
    }

    #endregion

    #region Update

    private static void OnStartUpdate()
    {
        UpdateManager.OnFixedUpdate += OnUpdate;
        IsActive = true;
    }

    private static void OnEndUpdate()
    {
        IsActive = false;
        UpdateManager.OnFixedUpdate -= OnUpdate;
    }

    private static void OnUpdate(int frameIndex, float deltaTime, float time)
    {
        foreach (var enemy in _enemies) enemy.OnUpdate(deltaTime, time);

        DoRegistrations();
    }

    #endregion
}