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

    [Header("References")]
    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private Weapon weapon;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;

    private float _health;

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
        _health = enemyType.MaxHealth;
        boxCollider.enabled = true;

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
        boxCollider.enabled = false;
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

    #region Damageable

    void IDamageable.TakeDamage(float damage)
    {
        if (!IsActive) return;

        _health -= damage;
        if (_health <= 0)
        {
            Dispose();
        }
    }

    #endregion
}
