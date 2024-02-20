using _2_Dev._1_Gameplay.Weapon;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected bool isPlayer;
    [SerializeField] protected Transform firePoint;

    [SerializeField] protected WeaponStrategy _weaponStrategy;
    [SerializeField] protected BulletStrategy _bulletStrategy;
    protected float _lastFireTime;

    public bool IsPlayer => isPlayer;

    public void SetStrategy(WeaponStrategy weaponStrategy, BulletStrategy bulletStrategy)
    {
        _weaponStrategy = weaponStrategy;
        _bulletStrategy = bulletStrategy;
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

        Bullet[] bullets = new Bullet[_weaponStrategy.BulletCount];
        Vector3 firePos = firePoint.position;
        Vector3 fireUp = firePoint.up;

        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Bullet.Get();
            float spreadRotation = _weaponStrategy.SpreadAngle * (i - bullets.Length / 2);
            Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadRotation) * fireUp;
            bullets[i].Init(firePos, spreadDirection, _bulletStrategy, this);
        }

        _lastFireTime = time;
    }
}
