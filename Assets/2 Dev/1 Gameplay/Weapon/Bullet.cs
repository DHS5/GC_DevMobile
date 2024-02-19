using UnityEngine;

namespace _2_Dev._1_Gameplay.Weapon
{
    public class Bullet : ScriptableObject
    {
        [SerializeField] int damage = 10;
        [SerializeField] float fireRate = 0.5f;
        [SerializeField] protected Sprite sprite;

        public int Damage => damage;
        public float FireRate => fireRate;

        public virtual void Initialize()
        {
            // no-op
        }
    }
}