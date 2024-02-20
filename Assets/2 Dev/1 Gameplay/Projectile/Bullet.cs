using _2_Dev._1_Gameplay.Weapon;
using System;
using UnityEngine;

public class Bullet : PoolableObject
{
    #region Global Members

    [SerializeField] private SpriteRenderer spriteRenderer;

    private BulletStrategy _strategy;
    private Vector3 _startDirection;
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
        _startDirection = dir;
        _strategy = strategy;
        _shooter = shooter;
        _startTime = Time.time;

        BulletManager.Register(this);
    }

    public void OnUpdate(float time)
    {
        float lifetime = time - _startTime;

        if (lifetime >= _strategy.Lifetime)
        {
            Dispose();
            return;
        }

        // Update Position and Rotation here
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsActive)
        {
            // Search for damageable, check if is Player etc...
        }
    }

    private void Dispose()
    {
        IsActive = false;
        BulletManager.Unregister(this);

        Pool.Dispose(this, Pool.PoolableType.BULLET);
    }

    /*
    Transform parent;

    public void SetSpeed(float speed) => this.speed = speed;
    public void SetParent(Transform parent) => this.parent = parent;

    public Action Callback;

    void Start()
    {
        if (muzzlePrefab != null)
        {
            // instantiate muzzle flash
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            muzzleVFX.transform.SetParent(parent);

            DestroyParticleSystem(muzzleVFX);

        }
    }

    void Update()
    {
        transform.SetParent(null);
        transform.position += transform.forward * (speed * Time.deltaTime);

        Callback?.Invoke();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hitPrefab != null)
        {
            // destroy particle system
            ContactPoint contact = collision.contacts[0];
            var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity);

            DestroyParticleSystem(hitVFX);
        }

        
        var plane = collision.gameObject.GetComponent<Plane>();
        if (plane != null)
        {
            plane.TakeDamage(10);
        }
        

        Destroy(gameObject);
    }

    void DestroyParticleSystem(GameObject vfx)
    {
        var ps = vfx.GetComponent<ParticleSystem>();
        if (ps == null)
        {
            ps = vfx.GetComponentInChildren<ParticleSystem>();
        }
        Destroy(vfx, ps.main.duration);
    }
*/
}