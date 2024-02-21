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
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private Weapon weapon;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D enemyRigidbody;

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

        enemyRigidbody.simulated = true;
        _enemyType = enemyType;
        IsActive = true;
        _health = enemyType.MaxHealth;

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
        splineAnimate.Pause();
        splineAnimate.Container = null;

        EnemyManager.Unregister(this);
        enemyRigidbody.simulated = false;

        IsActive = false;

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
        weapon.Shoot(time);
    }

    private void OnStaticUpdate(float time)
    {
        if (weapon != null)
        {
            
        }
    }

    public override void MoveTo(Vector3 position)
    {
        //enemyRigidbody.position = position;
        enemyTransform.position = position;
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
