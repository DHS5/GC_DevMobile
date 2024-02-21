using _2_Dev._1_Gameplay.Weapon;
using UnityEngine;

namespace _2_Dev._1_Gameplay.Colectible
{
    [CreateAssetMenu(fileName = "CollectibleData", menuName = "CollectibleData")]
    public class CollectibleData : ScriptableObject
    {
        [SerializeField] private CollectibleType type;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;

        [Space(5f)]

        [SerializeField] private float health;
        [SerializeField] private int bulletAddition;
        [SerializeField] private float spreadAddition;
        [SerializeField] private float fireRateAddition;
        [SerializeField] private BulletStrategy bulletStrategy;

        public Sprite Sprite => sprite;
        public Color Color => color;
        public CollectibleType Type => type;
        public float Health => health;
        public float SpreadAddition => spreadAddition;
        public int BulletAddition => bulletAddition;
        
        public float FireRateAddition => fireRateAddition;
        public BulletStrategy BulletStrategy => bulletStrategy;
    }
}