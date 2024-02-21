using System;
using UnityEngine;

namespace _2_Dev._1_Gameplay.Colectible
{
    public class Collectible : PoolableObject
    {
        [SerializeField] public CollectibleType Type { get; private set; }
        [SerializeField] public float Value { get; private set; }
        [SerializeField] public Collider2D Collider { get; private set; }

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