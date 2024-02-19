using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletManager
{
    #region Registration

    private static List<Bullet> _bullets = new();

    public static void Register(Bullet bullet)
    {
        if (!_bullets.Contains(bullet))
        {
            _bullets.Add(bullet);
        }
    }
    public static void Unregister(Bullet bullet)
    {
        _bullets.Remove(bullet);
    }

    private static void OnBulletsChange()
    {
        // Subscribe to update
    }

    #endregion
}
