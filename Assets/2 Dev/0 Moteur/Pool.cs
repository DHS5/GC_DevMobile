using System.Collections;
using System.Collections.Generic;
using _2_Dev._1_Gameplay.Colectible;
using UnityEngine;


public class PoolableObject : MonoBehaviour
{
    public virtual void MoveTo(Vector3 position)
    {
        transform.position = position;
    }
}

public class Pool : MonoBehaviour
{
    public enum PoolableType
    {
        BULLET = 0,
        ENEMY = 1,
        COLLECTIBLE = 2,
        VFX = 3,
    }

    #region Editor

#if UNITY_EDITOR

    private void OnValidate()
    {
        var poolPos = poolPosition.position;

        if (pooledBullets.IsValid())
            foreach (var bullet in pooledBullets)
                bullet.transform.position = poolPos;
        if (pooledEnemies.IsValid())
            foreach (var enemy in pooledEnemies)
                enemy.transform.position = poolPos;
        if (pooledCollectibles.IsValid())
            foreach (var collectible in pooledCollectibles)
                collectible.transform.position = poolPos;
        if (pooledVFXs.IsValid())
            foreach (var vfx in pooledVFXs)
                vfx.transform.position = poolPos;
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

    [Header("Pool")] [SerializeField] private Transform poolPosition;

    [Header("Bullet Pool")] [SerializeField]
    private List<Bullet> pooledBullets;

    [SerializeField] [Range(1, 100000)] private int bulletPoolBaseCapacity = 10000;
    [SerializeField] [Range(1, 10)] private int bulletPoolRefillLimit = 5;
    [SerializeField] [Range(1, 100)] private int bulletPoolRefillCapacity = 10;

    [Header("Enemy Pool")] [SerializeField]
    private List<Enemy> pooledEnemies;

    [SerializeField] [Range(1, 100000)] private int enemyPoolBaseCapacity = 10000;
    [SerializeField] [Range(1, 10)] private int enemyPoolRefillLimit = 5;
    [SerializeField] [Range(1, 100)] private int enemyPoolRefillCapacity = 10;

    [Header("Collectible Pool")] [SerializeField]
    private List<Collectible> pooledCollectibles;

    [SerializeField] [Range(1, 100000)] private int collectiblePoolBaseCapacity = 10;
    [SerializeField] [Range(1, 10)] private int collectiblePoolRefillLimit = 2;
    [SerializeField] [Range(1, 100)] private int collectiblePoolRefillCapacity = 1;
    
    [Header("VFX Pool")] [SerializeField]
    private List<VFX> pooledVFXs;

    [SerializeField] [Range(1, 100000)] private int vfxPoolBaseCapacity = 10;
    [SerializeField] [Range(1, 10)] private int vfxPoolRefillLimit = 2;
    [SerializeField] [Range(1, 100)] private int vfxPoolRefillCapacity = 1;

    #endregion

    #region Initialization

    private static Stack<Bullet> _bulletStack = new();
    private static Stack<Enemy> _enemyStack = new();
    private static Stack<Collectible> _collectibleStack = new();
    private static Stack<VFX> _vfxStack = new();

    private void Init()
    {
        // Stack Bullets
        if (pooledBullets.IsValid())
            foreach (var bullet in pooledBullets)
                _bulletStack.Push(bullet);
        var numberToCreate = bulletPoolBaseCapacity - pooledBullets.Count;
        if (numberToCreate > 0) CreateNewPoolables<Bullet>(PoolableType.BULLET, numberToCreate);

        // Stack Enemies
        if (pooledEnemies.IsValid())
            foreach (var enemy in pooledEnemies)
                _enemyStack.Push(enemy);
        numberToCreate = enemyPoolBaseCapacity - pooledEnemies.Count;
        if (numberToCreate > 0) CreateNewPoolables<Enemy>(PoolableType.ENEMY, numberToCreate);

        // Stack Collectibles
        if (pooledCollectibles.IsValid())
            foreach (var collectible in pooledCollectibles)
                _collectibleStack.Push(collectible);
        numberToCreate = collectiblePoolBaseCapacity - pooledCollectibles.Count;
        if (numberToCreate > 0) CreateNewPoolables<Collectible>(PoolableType.COLLECTIBLE, numberToCreate);
        
        // Stack VFX
        if (pooledVFXs.IsValid())
            foreach (var vfx in pooledVFXs)
                _vfxStack.Push(vfx);
        numberToCreate = vfxPoolBaseCapacity - pooledVFXs.Count;
        if (numberToCreate > 0) CreateNewPoolables<VFX>(PoolableType.VFX, numberToCreate);
    }

    #endregion

    #region Pooling

    public static T Get<T>(PoolableType type) where T : PoolableObject
    {
        if (GetStackCount(type) > 0)
        {
            var poolable = Pop<T>(type);
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
            case PoolableType.COLLECTIBLE: return _collectibleStack.Count;
            case PoolableType.VFX: return _vfxStack.Count;
        }

        return 0;
    }

    private static T Pop<T>(PoolableType type) where T : PoolableObject
    {
        switch (type)
        {
            case PoolableType.BULLET: return _bulletStack.Pop() as T;
            case PoolableType.ENEMY: return _enemyStack.Pop() as T;
            case PoolableType.COLLECTIBLE: return _collectibleStack.Pop() as T;
            case PoolableType.VFX: return _vfxStack.Pop() as T;
        }

        return null;
    }

    private static void Push<T>(PoolableType type, T newPoolable) where T : PoolableObject
    {
        switch (type)
        {
            case PoolableType.BULLET:
                _bulletStack.Push(newPoolable as Bullet);
                break;
            case PoolableType.ENEMY:
                _enemyStack.Push(newPoolable as Enemy);
                break;
            case PoolableType.COLLECTIBLE:
                _collectibleStack.Push(newPoolable as Collectible);
                break;
            case PoolableType.VFX:
                _vfxStack.Push(newPoolable as VFX);
                break;
        }
    }

    private static void EvaluateStack(PoolableType type)
    {
        if (!Exist()) return;
        switch (type)
        {
            case PoolableType.BULLET:
            {
                if (_bulletStack.Count <= I.bulletPoolRefillLimit)
                    CreateNewPoolables<Bullet>(type, I.bulletPoolRefillCapacity);
                break;
            }
            case PoolableType.ENEMY:
            {
                if (_enemyStack.Count <= I.enemyPoolRefillLimit)
                    CreateNewPoolables<Enemy>(type, I.enemyPoolRefillCapacity);
                break;
            }
            case PoolableType.COLLECTIBLE:
            {
                if (_collectibleStack.Count <= I.collectiblePoolRefillLimit)
                    CreateNewPoolables<Collectible>(type, I.collectiblePoolRefillCapacity);
                break;
            }
            case PoolableType.VFX:
            {
                if (_vfxStack.Count <= I.vfxPoolRefillLimit)
                    CreateNewPoolables<VFX>(type, I.vfxPoolRefillCapacity);
                break;
            }
        }
    }

    private static void CreateNewPoolables<T>(PoolableType type, int amount) where T : PoolableObject
    {
        T newPoolable;
        var poolPos = I.poolPosition.position;
        for (var i = 0; i < amount; i++)
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
            case PoolableType.COLLECTIBLE: return I.pooledCollectibles[0] as T;
            case PoolableType.VFX: return I.pooledVFXs[0] as T;
        }

        return null;
    }

    private static Transform GetParent(PoolableType type)
    {
        switch (type)
        {
            case PoolableType.BULLET: return I.pooledBullets[0].transform.parent;
            case PoolableType.ENEMY: return I.pooledEnemies[0].transform.parent;
            case PoolableType.COLLECTIBLE: return I.pooledCollectibles[0].transform.parent;
            case PoolableType.VFX: return I.pooledVFXs[0].transform.parent;
        }

        return null;
    }

    public static void Dispose<T>(T poolable, PoolableType type) where T : PoolableObject
    {
        if (!Exist() || poolable == null) return;
        poolable.MoveTo(I.poolPosition.position);
        Push(type, poolable);
    }

    #endregion
}