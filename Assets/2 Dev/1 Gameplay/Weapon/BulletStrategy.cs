﻿using UnityEngine;

namespace _2_Dev._1_Gameplay.Weapon
{
    [CreateAssetMenu(fileName = "Bullet Strategy", menuName = "Strategy/Bullet")]
    public class BulletStrategy : ScriptableObject
    {
        [SerializeField] int damage = 10;
        [SerializeField] float fireRate = 0.5f;
        [SerializeField] float lifetime = 5f;
        [SerializeField] protected Sprite sprite;

        public int Damage => damage;
        public float FireRate => fireRate;
        public float Lifetime => lifetime;
        public Sprite Sprite => sprite;
    }
}