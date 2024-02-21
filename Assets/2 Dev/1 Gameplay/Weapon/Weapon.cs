using _2_Dev._1_Gameplay.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField, Layer] protected int bulletLayer;

    [SerializeField] protected WeaponStrategy _weaponStrategy;
    [SerializeField] protected BulletStrategy _bulletStrategy;
    protected float _lastFireTime;

    private float _demiSpread;
    private int bulletCount;

    public int BulletLayer => bulletLayer;

    public void SetStrategy()
    {
        SetStrategy(_weaponStrategy, _bulletStrategy);
    }
    public void SetStrategy(WeaponStrategy weaponStrategy, BulletStrategy bulletStrategy)
    {
        _weaponStrategy = weaponStrategy;
        _bulletStrategy = bulletStrategy;
        _demiSpread = weaponStrategy.SpreadAngle / 2f;
        bulletCount = weaponStrategy.BulletCount;
        ComputeSpread();
    }

    private bool IsReadyToFire(float time)
    {
        return time - _lastFireTime >= _weaponStrategy.FireRate;
    }

    public void Shoot()
    {
        Shoot(Time.time);
    }
    public void Shoot(float time)
    {
        if (!IsReadyToFire(time)) return;

        Bullet[] bullets = new Bullet[bulletCount];
        Vector3 firePos = firePoint.position;
        float shootRot = firePoint.rotation.eulerAngles.z;

        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Bullet.Get();
            bullets[i].Init(firePos, spreads[i] - shootRot, _bulletStrategy, this);
        }

        _lastFireTime = time;
    }

    private float[] spreads;
    private void ComputeSpread()
    {
        spreads = new float[bulletCount];

        for (int i = 0; i  <bulletCount; i++)
        {
            spreads[i] = bulletCount == 1 ? 0 : Mathf.Lerp(-_demiSpread, _demiSpread, (float)i / (bulletCount - 1));
        }
    }
}
