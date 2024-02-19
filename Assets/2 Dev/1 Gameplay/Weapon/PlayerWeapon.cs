using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] InputReader input;
    float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= weaponStrategy.FireRate)
        {
            weaponStrategy.Fire(firePoint, layer);
            fireTimer = 0f;
        }
    }

}
