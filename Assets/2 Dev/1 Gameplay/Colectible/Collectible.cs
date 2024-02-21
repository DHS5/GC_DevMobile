using System;
using UnityEngine;

namespace _2_Dev._1_Gameplay.Colectible
{
    public class Collectible : PoolableObject
    {
        [Header("References")]
        [SerializeField] private Collider2D boxCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private CollectibleData _data;
        public bool IsActive { get; private set; }

        public static Collectible Get(CollectibleData data, Vector3 position)
        {
            Collectible c = Pool.Get<Collectible>(Pool.PoolableType.COLLECTIBLE);
            c.Init(data, position);
            return c;
        }

        public void Init(CollectibleData data, Vector3 position)
        {
            IsActive = true;
            _data = data;
            
            boxCollider.enabled = true;
            transform.position = position;
        }
        
        public void Dispose()
        {
            IsActive = false;
            boxCollider.enabled = false;
            
            Pool.Dispose(this, Pool.PoolableType.COLLECTIBLE);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (IsActive) return;

            if (other.gameObject.TryGetComponent<ICollectibleListener>(out var listener))
            {
                listener.OnCollectibleCollected(_data.Type, _data.Value);
                Dispose();
            }
        }
    }
}