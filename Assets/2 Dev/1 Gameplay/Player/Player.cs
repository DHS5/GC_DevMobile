using System;
using System.Collections.Generic;
using _2_Dev._1_Gameplay;
using _2_Dev._1_Gameplay.Colectible;
using _2_Dev._1_Gameplay.Weapon;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, ICollectibleListener
{
    
    [SerializeField] private Weapon weapon;
    [SerializeField] int maxHealth;
    [SerializeField] private float health;
    
    [SerializeField] private float relativeSize = 1;
    [SerializeField] private Vector2 relativeStartPosition = new Vector2(0, -0.5f);
    
    [SerializeField] private List<BulletStrategy> lvlBulletStrategies;
    [SerializeField] private int currentBulletStrategy;
    
    protected virtual void Awake() => health = maxHealth;
    
    protected void Die()
    {
        GameManager.Instance.GameOver();
    }
    
    void Start()
    {
        transform.SetRelativeSize(relativeSize, 1);
        transform.SetRelativePosition(relativeStartPosition);
        weapon.SetStrategy();
        currentBulletStrategy = 0;
    }

    private void OnEnable()
    {
        UpdateManager.OnUpdate += OnUpdate;
    }

    private void OnUpdate(int frameIndex, float deltaTime, float time)
    {
        weapon.Shoot(time);
    }

    private void OnDisable()
    {
        UpdateManager.OnUpdate -= OnUpdate;
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.PlaySFX("EnemyHit");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    public float GetHealthNormalized() => health / (float)maxHealth;
    public void OnCollectibleCollected(CollectibleData data)
    {
        CollectibleType type = data.Type;
        switch (type)
        {
            case CollectibleType.HEALTH:
                AddHealth((int)data.Health);
                break;
            case CollectibleType.SPREAD_PLUS:
            case CollectibleType.SPREAD_REDUCE:
            case CollectibleType.FIRE_RATE_PLUS:
            case CollectibleType.FIRE_RATE_REDUCE:
            case CollectibleType.BULLET_COUNT_PLUS:
                weapon.LevelUp(data.BulletAddition, data.SpreadAddition, data.FireRateAddition);
                break;
            case CollectibleType.BULLET_LEVEL_UP:
                currentBulletStrategy = Mathf.Clamp(currentBulletStrategy + 1, 0, lvlBulletStrategies.Count - 1);
                weapon.LevelUp(lvlBulletStrategies[currentBulletStrategy]);
                break;
            case CollectibleType.LEVEL_UP:
                GameManager.Instance.AddScore(data.ScoreAddition);
                break;
        }
    }
}
