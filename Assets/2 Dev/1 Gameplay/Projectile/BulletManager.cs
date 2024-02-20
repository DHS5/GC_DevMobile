using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletManager
{
    #region Registration

    private static List<Bullet> _bullets = new();
    private static bool _hasBullets = false;

    public static void Register(Bullet bullet)
    {
        if (!_bullets.Contains(bullet))
        {
            _bullets.Add(bullet);
            OnBulletsChange();
        }
    }
    public static void Unregister(Bullet bullet)
    {
        if (_bullets.Remove(bullet))
            OnBulletsChange();
    }

    private static void OnBulletsChange()
    {
        if (!_hasBullets && _bullets.Count > 0)
        {
            OnStartUpdate();
        }
        else if (_hasBullets && _bullets.Count == 0)
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
        foreach (var bullet in _bullets)
        {
            bullet.OnUpdate(deltaTime, time);
        }
    }

    #endregion
}
