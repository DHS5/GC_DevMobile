using UnityEngine.Events;
using UnityEngine;

public class EnemyHealth : Plane
{
    // [SerializeField] GameObject explosionPrefab;

    protected override void Die()
    {
        GameManager.Instance.AddScore(10);
        // TO CHANGE
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        OnSystemDestroyed?.Invoke();
        Destroy(gameObject);
    }

    public UnityEvent OnSystemDestroyed;
} 