using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : Weapon
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
