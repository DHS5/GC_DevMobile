using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _2_Dev._1_Gameplay.Weapon
{
    [CreateAssetMenu(fileName = "Bullet Strategy", menuName = "Strategy/Bullet")]
    public class BulletStrategy : ScriptableObject
    {
        [SerializeField] int damage = 10;
        [SerializeField] float lifetime = 5f;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected Color color;
        [SerializeField] protected float size;
        [SerializeField] protected String sfxName;
        

        [Space(15f)]

        [SerializeField] private AnimationCurve speedOverTime;
        [SerializeField] private float speed;
        [Space(5f)]
        [SerializeField] private AnimationCurve rotationOverTime;
        [SerializeField] private float rotation;

        public int Damage => damage;
        public float Lifetime => lifetime;
        public Sprite Sprite => sprite;
        public Color Color => color;
        public float Size => size;
        
        public String SfxName => sfxName;


        public float CurrentSpeed(float normalizedTime)
        {
            return speedOverTime.Evaluate(normalizedTime) * speed;
        }
        public float CurrentRotation(float normalizedTime)
        {
            return rotationOverTime.Evaluate(normalizedTime) * rotation;
        }
    }
}