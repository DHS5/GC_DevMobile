using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Strategy", menuName = "Strategy/Weapon")]
public abstract class WeaponStrategy : ScriptableObject
{
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int bulletCount = 1;

    public float FireRate => fireRate;
    public int BulletCount => bulletCount;

    /*
    public override void Fire(Transform firePoint, LayerMask layer)
    {
        for (int i = 0; i < 3; i++)
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.transform.Rotate(0f, spreadAngle * (i - 1), 0f);
            projectile.layer = layer;

            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetSpeed(projectileSpeed);

            Destroy(projectile, projectileLifetime);
        }
    }
    */
}
