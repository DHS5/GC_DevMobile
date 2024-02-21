using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Strategy", menuName = "Strategy/Weapon")]
public class WeaponStrategy : ScriptableObject
{
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int bulletCount = 1;
    [SerializeField] private float spreadAngle = 5f;

    public float FireRate => fireRate;
    public float SpreadAngle => spreadAngle;
    public int BulletCount => bulletCount;
}
