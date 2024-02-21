using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletManager
{
    #region Registration

    public static bool IsActive { get; private set; }

    private static List<Bullet> _bullets = new();
    private static bool _hasBullets = false;

    private static List<Bullet> _toRegister = new();
    private static List<Bullet> _toUnregister = new();

    public static void Register(Bullet bullet)
    {
        if (IsActive)
            _toRegister.Add(bullet);

        else
        {
            _bullets.Add(bullet);
            OnBulletsChange();
        }
    }
    public static void Unregister(Bullet bullet)
    {
        _toUnregister.Add(bullet);
    }

    private static void DoRegistrations()
    {
        bool change = false;

        if (_toRegister.IsValid())
        {
            change = true;
            foreach (var b in _toRegister)
            {
                _bullets.Add(b);
            }
        }
        if (_toUnregister.IsValid())
        {
            change = true;
            foreach (var b in _toUnregister)
            {
                _bullets.Remove(b);
            }
        }

        if (change)
        {
            OnBulletsChange();

            _toRegister.Clear();
            _toUnregister.Clear();
        }
    }

    private static void OnBulletsChange()
    {
        if (!_hasBullets && _bullets.Count > 0)
        {
            _hasBullets = true;
            OnStartUpdate();
        }
        else if (_hasBullets && _bullets.Count == 0)
        {
            _hasBullets = false;
            OnEndUpdate();
        }
    }

    private static void Clear()
    {
        _toRegister.Clear();
        _toUnregister.Clear();

        foreach (var bullet in _bullets)
        {
            if (bullet.IsActive)
            {
                bullet.Dispose();
            }
        }
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
        foreach (var bullet in _bullets)
        {
            bullet.OnUpdate(deltaTime, time);
        }

        DoRegistrations();
    }

    #endregion
}
