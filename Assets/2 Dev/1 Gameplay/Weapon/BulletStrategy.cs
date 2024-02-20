using UnityEngine;

namespace _2_Dev._1_Gameplay.Weapon
{
    [CreateAssetMenu(fileName = "Bullet Strategy", menuName = "Strategy/Bullet")]
    public class BulletStrategy : ScriptableObject
    {
        [SerializeField] int damage = 10;
        [SerializeField] float lifetime = 5f;
        [SerializeField] protected Sprite sprite;

        [Space(15f)]

        [SerializeField] private AnimationCurve speedOverTime;
        [SerializeField] private float speed;
        [Space(5f)]
        [SerializeField] private AnimationCurve rotationOverTime;
        [SerializeField] private float rotation;

        public int Damage => damage;
        public float Lifetime => lifetime;
        public Sprite Sprite => sprite;


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