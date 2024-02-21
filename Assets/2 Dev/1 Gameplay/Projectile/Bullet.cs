using _2_Dev._1_Gameplay.Weapon;
using System;
using _2_Dev._1_Gameplay;
using UnityEngine;

public class Bullet : PoolableObject
{
    #region Global Members

    [SerializeField] private Transform bulletTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D bulletRigidbody;

    private BulletStrategy _strategy;
    private Weapon _shooter;
    private float _startTime;

    public bool IsActive { get; private set; }

    #endregion

    #region Static Accessor

    public static Bullet Get() => Pool.Get<Bullet>(Pool.PoolableType.BULLET);

    #endregion

    public void Init(Vector3 pos, float dir, BulletStrategy strategy, Weapon shooter)
    {
        IsActive = true;

        gameObject.layer = shooter.BulletLayer;

        bulletRigidbody.position = pos;
        bulletRigidbody.rotation = dir;
        _strategy = strategy;
        _shooter = shooter;
        _startTime = Time.time;
        
        spriteRenderer.sprite = _strategy.Sprite;
        boxCollider.enabled = true;

        BulletManager.Register(this);
    }

    // Fixed Update ?
    public void OnUpdate(float deltaTime, float time)
    {
        float lifetime = time - _startTime;

        if (lifetime >= _strategy.Lifetime)
        {
            Dispose();
            return;
        }

        float normalizedLifetime = lifetime / _strategy.Lifetime;

        float rotateSpeed = deltaTime * _strategy.CurrentRotation(normalizedLifetime);
        if (rotateSpeed != 0)
            bulletRigidbody.MoveRotation(rotateSpeed);
            //bulletTransform.Rotate(Vector3.forward, rotateSpeed);
        float moveSpeed = deltaTime * _strategy.CurrentSpeed(normalizedLifetime);
        if (moveSpeed != 0)
            bulletRigidbody.Move(bulletTransform.up * moveSpeed);
            //bulletTransform.Move(bulletTransform.up * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActive) return;

        Debug.Log("collide with " + collision.gameObject, collision.gameObject);

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_strategy.Damage);
        }
    }

    private void Dispose()
    {
        boxCollider.enabled = false;
        IsActive = false;
        BulletManager.Unregister(this);

        Pool.Dispose(this, Pool.PoolableType.BULLET);
    }
}