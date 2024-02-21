using System;
using UnityEngine;

namespace _2_Dev._1_Gameplay.Colectible
{
    public class Collectible : PoolableObject
    {
        [SerializeField] private CollectibleType Type;
        [SerializeField] private float Value;
        [SerializeField] private Collider2D Collider;

        public void Init(CollectibleType type, float value)
        {
            Type = type;
            Value = value;
            
            Collider.enabled = true;
        }
        
        public void Dispose()
        {
            Collider.enabled = false;
            
            Pool.Dispose(this, Pool.PoolableType.COLLLECTIBLE);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<ICollectibleListener>(out var listener)) return;
            listener.OnCollectibleCollected(Type, Value);
            Dispose();
        }
    }
}