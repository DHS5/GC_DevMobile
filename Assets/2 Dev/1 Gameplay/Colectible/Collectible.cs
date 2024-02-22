using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _2_Dev._1_Gameplay.Colectible
{
    public class Collectible : PoolableObject
    {
        [FormerlySerializedAs("boxCollider")] [Header("References")] [SerializeField]
        private CircleCollider2D circleCollider;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private CollectibleData _data;
        public bool IsActive { get; private set; }

        public static Collectible Spawn(CollectibleData data, Vector3 position)
        {
            var c = Pool.Get<Collectible>(Pool.PoolableType.COLLECTIBLE);
            c.Init(data, position);
            return c;
        }

        public void Init(CollectibleData data, Vector3 position)
        {
            IsActive = true;
            _data = data;

            spriteRenderer.sprite = data.Sprite;
            spriteRenderer.color = data.Color;
            circleCollider.enabled = true;
            transform.position = position;
        }

        public void Dispose()
        {
            IsActive = false;
            circleCollider.enabled = false;

            Pool.Dispose(this, Pool.PoolableType.COLLECTIBLE);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (!IsActive) return;

            if (other.gameObject.TryGetComponent<ICollectibleListener>(out var listener))
            {
                listener.OnCollectibleCollected(_data);
                Dispose();
            }
        }
    }
}