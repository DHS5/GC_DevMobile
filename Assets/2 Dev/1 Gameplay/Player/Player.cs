using _2_Dev._1_Gameplay;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    
    [SerializeField] private Weapon weapon;
    [SerializeField] int maxHealth;
    [SerializeField] private float health;
    
    protected virtual void Awake() => health = maxHealth;
    
    protected void Die()
    {
        GameManager.Instance.GameOver();
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
