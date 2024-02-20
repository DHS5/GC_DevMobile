using _2_Dev._1_Gameplay.Weapon;
using System;
using _2_Dev._1_Gameplay;
using UnityEngine;

public class Bullet : PoolableObject
{
    #region Global Members

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;

    private BulletStrategy _strategy;
    private Weapon _shooter;
    private float _startTime;

    public bool IsActive { get; private set; }

    #endregion

    #region Static Accessor

    public static Bullet Get() => Pool.Get<Bullet>(Pool.PoolableType.BULLET);

    #endregion

    public void Init(Vector3 pos, Vector3 dir, BulletStrategy strategy, Weapon shooter)
    {
        IsActive = true;

        transform.position = pos;
        transform.up = dir;
        _strategy = strategy;
        _shooter = shooter;
        _startTime = Time.time;
        
        spriteRenderer.sprite = _strategy.Sprite;
        boxCollider.enabled = true;

        BulletManager.Register(this);
    }

    public void OnUpdate(float deltaTime, float time)
    {
        float lifetime = time - _startTime;

        if (lifetime >= _strategy.Lifetime)
        {
            Dispose();
        }

        float normalizedLifetime = lifetime / _strategy.Lifetime;

        transform.Rotate(Vector3.forward, deltaTime * _strategy.CurrentRotation(normalizedLifetime));
        transform.Move(transform.up * (deltaTime * _strategy.CurrentSpeed(normalizedLifetime)));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActive) return;

        Debug.Log("collide with " + collision.gameObject);

        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.TakeDamage(10);
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