using _2_Dev._1_Gameplay.Weapon;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;

    protected WeaponStrategy _weaponStrategy;
    protected BulletStrategy _bulletStrategy;

    protected int Layer => gameObject.layer;

    public void SetStrategy(WeaponStrategy weaponStrategy, BulletStrategy bulletStrategy)
    {
        _weaponStrategy = weaponStrategy;
        _bulletStrategy = bulletStrategy;
    }

    public void Shoot()
    {

    }
}
