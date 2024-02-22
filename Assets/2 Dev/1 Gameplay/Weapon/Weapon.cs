using _2_Dev._1_Gameplay.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] [Layer] protected int bulletLayer;
    
    [SerializeField] private AudioData shootSoundData;

    [SerializeField] protected WeaponStrategy _weaponStrategy;
    [SerializeField] protected BulletStrategy _bulletStrategy;
    protected float _lastFireTime;

    private float _fireRate;
    private float _spread;
    private float _demiSpread;
    private int _bulletCount;

    public int BulletLayer => bulletLayer;

    public void SetStrategy()
    {
        SetStrategy(_weaponStrategy, _bulletStrategy);
    }

    public void SetStrategy(WeaponStrategy weaponStrategy, BulletStrategy bulletStrategy)
    {
        _weaponStrategy = weaponStrategy;
        _bulletStrategy = bulletStrategy;
        _fireRate = weaponStrategy.FireRate;
        _spread = weaponStrategy.SpreadAngle;
        _demiSpread = _spread / 2f;
        _bulletCount = weaponStrategy.BulletCount;
        ComputeSpread();
    }

    public void LevelUp(int bulletCountAddition, float spreadAddition, float fireRatePercent)
    {
        if (fireRatePercent != 0) _fireRate *= fireRatePercent;
        _bulletCount += bulletCountAddition;
        _spread += spreadAddition;
        _demiSpread = _spread / 2f;
        ComputeSpread();
    }

    public void LevelUp(BulletStrategy bulletStrategy)
    {
        _bulletStrategy = bulletStrategy;
    }

    private bool IsReadyToFire(float time)
    {
        return time - _lastFireTime >= _fireRate;
    }

    public void Shoot()
    {
        Shoot(Time.time);
    }

    public void Shoot(float time)
    {
        if (!IsReadyToFire(time)) return;
        
        AudioManager.Instance.PlayShootSFX(shootSoundData);
        
        Bullet bullet;
        var firePos = firePoint.position;
        var shootRot = firePoint.rotation.eulerAngles.z;

        for (var i = 0; i < _bulletCount; i++)
        {
            bullet = Bullet.Get();
            bullet.Init(firePos, spreads[i] - shootRot, _bulletStrategy, this);
        }

        _lastFireTime = time;
    }

    private float[] spreads;

    private void ComputeSpread()
    {
        spreads = new float[_bulletCount];

        for (var i = 0; i < _bulletCount; i++)
            spreads[i] = _bulletCount == 1 ? 0 : Mathf.Lerp(-_demiSpread, _demiSpread, (float)i / (_bulletCount - 1));
    }
}