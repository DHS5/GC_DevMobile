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

    #region Global Members

    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private Weapon weapon;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private EnemyType _enemyType;


    public bool IsActive { get; private set; }

    #endregion

    #region Static Accessor

    public Enemy Get()
    {
        return Pool.Get<Enemy>(Pool.PoolableType.ENEMY);
    }

    #endregion

    public void Init(EnemyType enemyType)
    {
        _enemyType = enemyType;
        IsActive = true;

        if (enemyType.Movement == EnemyMovement.PATH)
        {
            splineAnimate.Container = SplineManager.GetSplineContainer(enemyType.Path);
        }
        else
        {
            transform.SetRelativePosition(enemyType.FixedRelativePosition);
        }
        weapon.SetStrategy(_enemyType.WeaponStrategy, _enemyType.BulletStrategy);
    }

    public void Dispose()
    {
        IsActive = false;


        Pool.Dispose<Enemy>(this, Pool.PoolableType.ENEMY);
    }


    public void OnUpdate(float deltaTime, float time)
    {
        switch (_enemyType.Movement)
        {
            case EnemyMovement.PATH:
                break;
            case EnemyMovement.STATIC:
                OnStaticUpdate(time);
                break;
        }
    }

    private void OnStaticUpdate(float time)
    {
        if (weapon != null && weapon.IsReadyToFire(time))
        {
            weapon.Shoot(time);
        }
    }
}
