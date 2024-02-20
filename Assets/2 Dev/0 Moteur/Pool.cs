using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolableObject : MonoBehaviour { }

public class Pool : MonoBehaviour
{
    public enum PoolableType
    {
        BULLET = 0,
        ENEMY = 1,

    }

    #region Editor

#if UNITY_EDITOR

    private void OnValidate()
    {
        Vector3 poolPos = poolPosition.position;

        if (pooledBullets.IsValid())
        {
            foreach (var bullet in pooledBullets)
            {
                bullet.transform.position = poolPos;
            }
        }
    }

#endif

    #endregion

    #region Utility

    private static bool Exist()
    {
        if (I != null) return true;

        Debug.LogError("Pool doesn't exist in scene");
        return false;
    }

    #endregion

    #region Singleton

    private static Pool I { get; set; }

    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }
        I = this;

        Init();
    }

    #endregion

    #region Global Members

    [Header("Pool")]
    [SerializeField] private Transform poolPosition;

    [Header("Bullet Pool")]
    [SerializeField] private List<Bullet> pooledBullets;
    [SerializeField][Range(1, 100000)] private int bulletPoolBaseCapacity = 10000;
    [SerializeField][Range(1, 10)] private int bulletPoolRefillLimit = 5;
    [SerializeField][Range(1, 100)] private int bulletPoolRefillCapacity = 10;
    
    [Header("Enemy Pool")]
    [SerializeField] private List<Enemy> pooledEnemies;
    [SerializeField][Range(1, 100000)] private int enemyPoolBaseCapacity = 10000;
    [SerializeField][Range(1, 10)] private int enemyPoolRefillLimit = 5;
    [SerializeField][Range(1, 100)] private int enemyPoolRefillCapacity = 10;

    #endregion

    #region Initialization

    private static Stack<Bullet> _bulletStack = new();
    private static Stack<Enemy> _enemyStack = new();

    private void Init()
    {
        // Stack Bullets
        if (pooledBullets.IsValid())
        {
            foreach (var bullet in pooledBullets)
            {
                _bulletStack.Push(bullet);
            }
        }
        int numberToCreate = bulletPoolBaseCapacity - pooledBullets.Count;
        if (numberToCreate > 0) CreateNewPoolables<Bullet>(PoolableType.BULLET, numberToCreate);
        
        // Stack Enemies
        if (pooledEnemies.IsValid())
        {
            foreach (var enemy in pooledEnemies)
            {
                _enemyStack.Push(enemy);
            }
        }
        numberToCreate = enemyPoolBaseCapacity - pooledEnemies.Count;
        if (numberToCreate > 0) CreateNewPoolables<Enemy>(PoolableType.ENEMY, numberToCreate);
    }

    #endregion

    #region Pooling

    public static T Get<T>(PoolableType type) where T : PoolableObject
    {
        if (GetStackCount(type) > 0)
        {
            T poolable = Pop<T>(type);
            EvaluateStack(type);
            return poolable;
        }

        Debug.LogError("Stack empty");
        return null;
    }

    private static int GetStackCount(PoolableType type)
    {
        switch (type)
        {
            case PoolableType.BULLET: return _bulletStack.Count;
            case PoolableType.ENEMY: return _enemyStack.Count;
        }
        return 0;
    }

    private static T Pop<T>(PoolableType type) where T : PoolableObject
    {
        switch (type)
        {
            case PoolableType.BULLET: return _bulletStack.Pop() as T;
            case PoolableType.ENEMY: return _enemyStack.Pop() as T;
        }
        return null;
    }
    private static void Push<T>(PoolableType type, T newPoolable) where T : PoolableObject
    {
        switch (type)
        {
            case PoolableType.BULLET: _bulletStack.Push(newPoolable as Bullet); break;
            case PoolableType.ENEMY: _enemyStack.Push(newPoolable as Enemy); break;
        }
    }

    private static void EvaluateStack(PoolableType type)
    {
        if (Exist())
        {
            switch (type)
            {
                case PoolableType.BULLET:
                    {
                        if (_bulletStack.Count <= I.bulletPoolRefillLimit)
                        {
                            CreateNewPoolables<Bullet>(type, I.bulletPoolRefillCapacity);
                        }
                        break;
                    }
                case PoolableType.ENEMY:
                    {
                        if (_enemyStack.Count <= I.enemyPoolRefillLimit)
                        {
                            CreateNewPoolables<Enemy>(type, I.enemyPoolRefillCapacity);
                        }
                        break;
                    }
            }
        }
    }

    private static void CreateNewPoolables<T>(PoolableType type, int amount) where T : PoolableObject
    {
        T newPoolable;
        Vector3 poolPos = I.poolPosition.position;
        for (int i = 0; i < amount; i++)
        {
            newPoolable = Instantiate(GetOriginal<T>(type), GetParent(type));
            newPoolable.transform.position = poolPos;
            Push(type, newPoolable);
        }
    }

    private static T GetOriginal<T>(PoolableType type) where T : PoolableObject
    {
        switch (type)
        {
            case PoolableType.BULLET: return I.pooledBullets[0] as T;
            case PoolableType.ENEMY: return I.pooledEnemies[0] as T;
        }
        return null;
    }
    private static Transform GetParent(PoolableType type)
    {
        switch (type)
        {
            case PoolableType.BULLET: return I.pooledBullets[0].transform.parent;
            case PoolableType.ENEMY: return I.pooledEnemies[0].transform.parent;
        }
        return null;
    }

    public static void Dispose<T>(T poolable, PoolableType type) where T : PoolableObject
    {
        if (Exist() && poolable != null)
        {
            poolable.transform.position = I.poolPosition.position;
            Push(type, poolable);
        }
    }

    #endregion
}
