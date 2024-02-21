using _2_Dev._1_Gameplay.Weapon;
using System;
using _2_Dev._1_Gameplay;
using UnityEngine;

public class Bullet : PoolableObject
{
    #region Global Members

    [SerializeField] private Transform bulletTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D boxCollider;
    [SerializeField] private Rigidbody2D bulletRigidbody;

    private BulletStrategy _strategy;
    private float _startTime;
    private float _currentRotation;

    public bool IsActive { get; private set; }

    #endregion

    #region Static Accessor

    public static Bullet Get() => Pool.Get<Bullet>(Pool.PoolableType.BULLET);

    #endregion

    public void Init(Vector3 pos, float dir, BulletStrategy strategy, Weapon shooter)
    {
        IsActive = true;

        gameObject.layer = shooter.BulletLayer;
        bulletTransform.position = pos;

        bulletRigidbody.simulated = true;
        _currentRotation = dir;
        bulletRigidbody.rotation = _currentRotation;
        _strategy = strategy;
        _startTime = Time.time;

        spriteRenderer.sprite = _strategy.Sprite;
        spriteRenderer.color = _strategy.Color;
        spriteRenderer.transform.SetRelativeSize(_strategy.Size, 1);

        BulletManager.Register(this);
    }

    public void OnUpdate(float deltaTime, float time)
    {
        float lifetime = time - _startTime;

        if (lifetime >= _strategy.Lifetime)
        {
            Dispose();
            return;
        }

        float normalizedLifetime = lifetime / _strategy.Lifetime;

        float rotateSpeed = _strategy.CurrentRotation(normalizedLifetime);
        if (rotateSpeed != 0)
        {
            _currentRotation += deltaTime * rotateSpeed;
            bulletRigidbody.MoveRotation(_currentRotation);
        }
        float moveSpeed = _strategy.CurrentSpeed(normalizedLifetime);
        if (moveSpeed != 0)
            bulletRigidbody.Move(deltaTime * moveSpeed * bulletTransform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActive) return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_strategy.Damage);
            Dispose();
        }
    }

    public void Dispose()
    {
        BulletManager.Unregister(this);
        bulletRigidbody.simulated = false;

        IsActive = false;

        Pool.Dispose(this, Pool.PoolableType.BULLET);
    }

    public override void MoveTo(Vector3 position)
    {
        //bulletRigidbody.MovePosition(position);
        bulletTransform.position = position;
    }
}