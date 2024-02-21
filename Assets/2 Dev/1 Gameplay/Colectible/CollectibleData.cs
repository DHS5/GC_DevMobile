using UnityEngine;
using UnityEngine.Serialization;

namespace _2_Dev._1_Gameplay.Colectible
{
    public class CollectibleData : ScriptableObject
    {
        [SerializeField] private CollectibleType type;
        [SerializeField] private float value;
        [SerializeField] private Sprite sprite;

        public Sprite Sprite => sprite;
        public CollectibleType Type => type;
        public float Value => value;
    }
}