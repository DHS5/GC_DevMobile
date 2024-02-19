using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= weaponStrategy.FireRate)
        {
            weaponStrategy.Fire(firePoint, Layer);
            fireTimer = 0f;
        }
    }
}
