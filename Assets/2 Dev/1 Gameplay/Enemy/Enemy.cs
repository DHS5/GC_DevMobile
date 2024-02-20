using System.Collections;
using System.Collections.Generic;
using _2_Dev._1_Gameplay;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

public class Enemy : PoolableObject, IDamageable
{
    public enum EnemyMovement
    {
        PATH,
        STATIC
    }

    #region Global Members

    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float health;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private EnemyType _enemyType;


    public bool IsActive { get; private set; }

    #endregion

    #region Static Accessor

    public static Enemy Spawn(EnemyType type)
    {
        Enemy e = Pool.Get<Enemy>(Pool.PoolableType.ENEMY);
        e.Init(type);
        return e;
    }

    #endregion

    public void Init(EnemyType enemyType)
    {
        EnemyManager.Register(this);

        _enemyType = enemyType;
        IsActive = true;

        if (enemyType.Movement == EnemyMovement.PATH)
        {
            splineAnimate.Container = SplineManager.GetSplineContainer(enemyType.Path);
            splineAnimate.Restart(true);
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
        EnemyManager.Unregister(this);

        Pool.Dispose(this, Pool.PoolableType.ENEMY);
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
        if (weapon != null)
        {
            weapon.Shoot(time);
        }
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Dispose();
        }
    }
}
