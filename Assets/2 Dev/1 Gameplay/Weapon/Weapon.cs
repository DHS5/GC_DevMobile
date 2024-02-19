using _2_Dev._1_Gameplay.Weapon;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected bool isPlayer;
    [SerializeField] protected Transform firePoint;

    protected WeaponStrategy _weaponStrategy;
    protected BulletStrategy _bulletStrategy;


    public bool IsPlayer => isPlayer;

    public void SetStrategy(WeaponStrategy weaponStrategy, BulletStrategy bulletStrategy)
    {
        _weaponStrategy = weaponStrategy;
        _bulletStrategy = bulletStrategy;
    }

    public void Shoot()
    {
        Bullet[] bullets = new Bullet[_weaponStrategy.BulletCount];
        Vector3 firePos = firePoint.position;
        Vector3 fireRight = firePoint.right;

        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Bullet.Get();
            bullets[i].Init(firePos, Vector3.Slerp(fireRight, -fireRight, (i + 1)/(bullets.Length + 1)), _bulletStrategy, this);
        }
    }
}
