using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : PoolableObject
{
    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private Weapon weapon;

    private EnemyType _enemyType;

    public void Init(EnemyType enemyType)
    {
        _enemyType = enemyType;

        splineAnimate.Container = SplineManager.GetSplineContainer(enemyType.Path);
        weapon.SetStrategy(_enemyType.WeaponStrategy, _enemyType.BulletStrategy);
    }
}
