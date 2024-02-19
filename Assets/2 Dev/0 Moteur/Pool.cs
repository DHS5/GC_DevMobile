using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    #region Editor

#if UNITY_EDITOR

    private void OnValidate()
    {
        Vector3 poolPos = poolPosition.position;

        if (pooledSpriteRenderers.IsValid())
        {
            foreach (var sr in pooledSpriteRenderers)
            {
                sr.transform.position = poolPos;
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

    [Header("SpriteRenderer Pool")]
    [SerializeField] private List<SpriteRenderer> pooledSpriteRenderers;
    [SerializeField][Range(1, 100000)] private int srPoolBaseCapacity = 10000;
    [SerializeField][Range(1, 10)] private int srPoolRefillLimit = 5;
    [SerializeField][Range(1, 100)] private int srPoolRefillCapacity = 10;

    #endregion

    #region Initialization

    private static Stack<SpriteRenderer> _spriteRendererStack = new();

    private void Init()
    {
        // Stack Sprite Renderer
        if (pooledSpriteRenderers.IsValid())
        {
            foreach (var psr in pooledSpriteRenderers)
            {
                _spriteRendererStack.Push(psr);
            }
        }
        int numberToCreate = srPoolBaseCapacity - pooledSpriteRenderers.Count;
        if (numberToCreate > 0) CreateNewSpriteRenderers(numberToCreate);
    }

    #endregion

    #region Pooling

    public static SpriteRenderer GetSpriteRenderer()
    {
        if (_spriteRendererStack.Count > 0)
        {
            SpriteRenderer sr = _spriteRendererStack.Pop();
            EvaluateSpriteRendererStack();
            return sr;
        }
        else
        {
            Debug.LogError("Sprite Renderer Stack empty");
            return null;
        }
    }
    public static void DisposeSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        if (Exist() && spriteRenderer != null)
        {
            spriteRenderer.transform.position = I.poolPosition.position;
            _spriteRendererStack.Push(spriteRenderer);
        }
    }

    private static void EvaluateSpriteRendererStack()
    {
        if (Exist() && _spriteRendererStack.Count <= I.srPoolRefillLimit)
        {
            CreateNewSpriteRenderers(I.srPoolRefillCapacity);
            Debug.Log("Increase Sprite Renderer pool capacity by " + I.srPoolRefillCapacity);
        }
    }
    private static void CreateNewSpriteRenderers(int amount)
    {
        SpriteRenderer sr;
        Vector3 poolPos = I.poolPosition.position;
        for (int i = 0; i < amount; i++)
        {
            sr = Instantiate(I.pooledSpriteRenderers[0], I.pooledSpriteRenderers[0].transform.parent);
            sr.transform.position = poolPos;
            _spriteRendererStack.Push(sr);
        }
    }

    #endregion
}
