using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : PoolableObject
{
    public enum EnemyMovement
    {
        PATH,
        STATIC
    }

    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private Weapon weapon;

    private EnemyType _enemyType;

    public void Init(EnemyType enemyType)
    {
        _enemyType = enemyType;

        splineAnimate.Container = SplineManager.GetSplineContainer(enemyType.Path);
        weapon.SetStrategy(_enemyType.WeaponStrategy, _enemyType.BulletStrategy);
    }


    public void OnUpdate(float deltaTime)
    {
        switch (_enemyType.Movement)
        {
            case EnemyMovement.PATH:
                break;
            case EnemyMovement.STATIC:
                OnStaticUpdate(deltaTime);
                break;
        }
    }

    private void OnStaticUpdate(float deltaTime)
    {

    }
}
