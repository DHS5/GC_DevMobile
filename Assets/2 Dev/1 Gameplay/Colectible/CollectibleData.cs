using _2_Dev._1_Gameplay.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private int scoreAddition;

        public Sprite Sprite => sprite;
        public Color Color => color;
        public CollectibleType Type => type;
        public float Health => health;
        public float SpreadAddition => spreadAddition;
        public int BulletAddition => bulletAddition;
        public int ScoreAddition => scoreAddition;
        
        public float FireRateAddition => fireRateAddition;
    }
}