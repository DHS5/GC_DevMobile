using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponStrategy weaponStrategy;
    [SerializeField] protected Transform firePoint;

    protected int Layer => gameObject.layer;

    void Start() => weaponStrategy.Initialize();

    public void SetWeaponStrategy(WeaponStrategy strategy)
    {
        weaponStrategy = strategy;
        weaponStrategy.Initialize();
    }
}