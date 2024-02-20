using System;
using _2_Dev._1_Gameplay;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    
    [SerializeField] private Weapon weapon;
    [SerializeField] int maxHealth;
    [SerializeField] private float health;
    
    [SerializeField] private float relativeSize = 1;
    [SerializeField] private Vector2 relativeStartPosition = new Vector2(0, -0.5f);
    
    protected virtual void Awake() => health = maxHealth;
    
    protected void Die()
    {
        GameManager.Instance.GameOver();
    }
    
    void Start()
    {
        transform.SetRelativeSize(relativeSize, 1);
        transform.SetRelativePosition(relativeStartPosition);
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
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    public float GetHealthNormalized() => health / (float)maxHealth;
}
